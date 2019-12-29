using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atak
{
    public partial class FileExplorer : Form
    {
        Socket socket;    
        public FileExplorer(Socket socket)
        {
            this.socket = socket;
            InitializeComponent();
        }

        private void FileExplorer_Load(object sender, EventArgs e)
        {
            txtPath.Text = @"C:\";
            socket.Send(Encoding.UTF8.GetBytes("GETPATH|" + @"C:\"));            
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                txtPath.Text = listBox1.SelectedItem.ToString();
                socket.Send(Encoding.UTF8.GetBytes("GETPATH|" + listBox1.SelectedItem.ToString()));
                listBox1.Items.Clear();
                listBox2.Items.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            socket.Send(Encoding.UTF8.GetBytes("GETPATH|" + @txtPath.Text));
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtPath.Text.Length != 3)
            {
                txtPath.Text = Directory.GetParent(@txtPath.Text).FullName;
                socket.Send(Encoding.UTF8.GetBytes("GETPATH|" + @txtPath.Text));
                listBox1.Items.Clear();
                listBox2.Items.Clear();
            }         
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                socket.Send(Encoding.UTF8.GetBytes("START|" + listBox2.SelectedItem.ToString()));
            }
            else
            {
                MessageBox.Show("Select item to start", "Atak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                socket.Send(Encoding.UTF8.GetBytes("DOWN|" + listBox2.SelectedItem.ToString()));
            }
            else
            {
                MessageBox.Show("Select item to download", "Atak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void createFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string folderName = Interaction.InputBox("New folder name to create", "Atak");
            if (!string.IsNullOrEmpty(folderName))
            {
                socket.Send(Encoding.UTF8.GetBytes("CFOLDER|" + txtPath.Text + "\\" + folderName));
                socket.Send(Encoding.UTF8.GetBytes("GETPATH|" + @txtPath.Text));
                listBox1.Items.Clear();
                listBox2.Items.Clear();
            }           
        }

        private void createFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = Interaction.InputBox("New file name to create ex:(file.txt)", "Atak");
            if (!string.IsNullOrEmpty(fileName))
            {
                socket.Send(Encoding.UTF8.GetBytes("CFILE|" + txtPath.Text + "\\" + fileName));
                socket.Send(Encoding.UTF8.GetBytes("GETPATH|" + @txtPath.Text));
                listBox1.Items.Clear();
                listBox2.Items.Clear();
            }
        }
    }
}
