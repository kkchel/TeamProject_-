using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace TeamProject
{
    public partial class Form1 : Form
    {
        public Client client;
        public Server server;
        IPAddress thisAddress;
        public Form1()
        {
            InitializeComponent();
            client = new Client();
            server = new Server();
            FormLoaded();
        }

        private void FormLoaded()
        {
            IPHostEntry he = Dns.GetHostEntry(Dns.GetHostName());

            // 처음으로 발견되는 ipv4 주소를 사용한다.
            foreach (IPAddress addr in he.AddressList)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    thisAddress = addr;
                    break;
                }
            }

            // 주소가 없다면..
            if (thisAddress == null)
                // 로컬호스트 주소를 사용한다.
                thisAddress = IPAddress.Loopback;

            txtIP.Text = thisAddress.ToString();
        }

        private void Input(object sender, EventArgs e)
        {
            if (nick_Box.Text == "" || txtIP.Text == "" || txtPort.Text == "")
            {
                MessageBox.Show("빈칸이 있습니다.");
                return;
            }

            if (client.ConnToServer(txtIP.Text, txtPort.Text, nick_Box.Text))
            {
                client.Show();
                this.Opacity = 0;
                this.Enabled = true;
            }
            else
            {
                txtPort.Focus();
                txtPort.SelectAll();
            }
        }

        private void Open(object sender, EventArgs e)
        {
            if (nick_Box.Text == "" || txtIP.Text == "" || txtPort.Text == "")
            {
                MessageBox.Show("빈칸이 있습니다.");
                return;
            }

            if (server.RunServer(txtIP.Text, txtPort.Text))
            {
                server.Show();
                server.timer1.Start();
                client.ConnToServer(txtIP.Text, txtPort.Text, nick_Box.Text);
                client.Show();

                
                this.Opacity = 0;
                this.Enabled = true;
            }
        }
    }
}
