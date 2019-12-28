using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Atak
{
    public partial class Message : Form
    {
        Socket socket;
        public Message(Socket socket)
        {
            this.socket = socket;
            InitializeComponent();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            switch (comboBoxStyle.SelectedItem.ToString())
            {
                case "Information":
                    socket.Send(Encoding.UTF8.GetBytes($"MSG|{txtTitle.Text}|{txtContent.Text}|INFO"));
                    break;
                case "Question":
                    socket.Send(Encoding.UTF8.GetBytes($"MSG|{txtTitle.Text}|{txtContent.Text}|QUES"));
                    break;
                case "Warning":
                    socket.Send(Encoding.UTF8.GetBytes($"MSG|{txtTitle.Text}|{txtContent.Text}|WARN"));
                    break;
                case "Error":
                    socket.Send(Encoding.UTF8.GetBytes($"MSG|{txtTitle.Text}|{txtContent.Text}|INF"));
                    break;
            }
        }

        private void Message_Load(object sender, EventArgs e)
        {
            comboBoxStyle.SelectedIndex = 0;
        }
    }
}
