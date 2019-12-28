using System;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Atak
{
    public partial class Screen : Form
    {
        Socket socket;
        public Screen(Socket socket)
        {
            this.socket = socket;
            InitializeComponent();
        }

        private void Screen_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                socket.Send(Encoding.UTF8.GetBytes("SCR|0"));
            }
            catch (Exception)
            {
            }        
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            
            
            Point coord = me.Location;
            socket.Send(Encoding.UTF8.GetBytes($"CLICK|{coord.X * 2}|{coord.Y * 2}"));
        }
    }
}
