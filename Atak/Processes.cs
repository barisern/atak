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

        }

        private void startNewProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refresh();
        }

        void refresh()
        {
            socket.Send(Encoding.UTF8.GetBytes("GETPRC"));
        }
    }
}
