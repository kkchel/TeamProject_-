using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace TeamProject
{
    public partial class Client : Form
    {
        delegate void AppendTextDelegate(Control ctrl, string s);
        AppendTextDelegate _textAppender;
        Socket mainSock;
        Point p1;
        Point p2;

        string pp1;
        string pp2;

        string nick;
        Color clr = Color.Black;
        float p_width = 2;
        int road;
        public Client()
        {
            InitializeComponent();
            mainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _textAppender = new AppendTextDelegate(AppendText);

            

        }

        void AppendText(Control ctrl, string s)
        {
            if (ctrl.InvokeRequired) ctrl.Invoke(_textAppender, ctrl, s);
            else
            {
                string source = ctrl.Text;
                if (source == "")
                    ctrl.Text = source + s;
                else
                    ctrl.Text = source + Environment.NewLine + s;
            }
        }

        public Boolean ConnToServer(string tmpip, string tmpPort, string nick)
        {
            this.nick = nick;
            if (mainSock.Connected)
            {
                MessageBox.Show("이미 연결되어 있습니다!");
                return false;
            }

            int port;
            if (!int.TryParse(tmpPort, out port))
            {
                MessageBox.Show("포트 번호가 잘못 입력되었거나 입력되지 않았습니다.");
                return false;
            }

            try { mainSock.Connect(tmpip, port); }
            catch (Exception ex)
            {
                MessageBox.Show("연결에 실패했습니다!", ex.Message);
                return false;
            }

            // 연결 완료되었다는 메세지를 띄워준다.
            AppendText(txtChat, "서버와 연결되었습니다.");

            // 연결 완료, 서버에서 데이터가 올 수 있으므로 수신 대기한다.
            AsyncObj obj = new AsyncObj(4096);
            obj.WorkingSocket = mainSock;
            mainSock.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);

            return true;
        }

        void DataReceived(IAsyncResult ar)
        {
            // BeginReceive에서 추가적으로 넘어온 데이터를 AsyncObject 형식으로 변환한다.
            AsyncObj obj = (AsyncObj)ar.AsyncState;
            int received = 0;
            // 데이터 수신을 끝낸다.
            try
            {
                received = obj.WorkingSocket.EndReceive(ar);
            }
            catch
            {
                picBox.Enabled = !true;
                txtTTS.Enabled = !true;
                AppendText(txtChat, "서버가 닫혔습니다.");
                obj.WorkingSocket.Close();
                return;
            }
            // 받은 데이터가 없으면(연결끊어짐) 끝낸다.
            if (received <= 0)
            {
                obj.WorkingSocket.Close();
                return;
            }
            // 텍스트로 변환한다.

            string text = Encoding.UTF8.GetString(obj.Buffer);

            string[] tokens = text.Split('\x01');
            string type = tokens[0];

            if (type == "0")
            {
                string ip = tokens[1];
                string msg = tokens[2];
                AppendText(txtChat, string.Format("[받음]{0}: {1}", ip, msg));
            }

            else if (type == "1")
            {
                string point1 = tokens[1];
                string[] pts1 = point1.Split(',');

                int x1 = int.Parse(pts1[0]);
                int y1 = int.Parse(pts1[1]);

                Draw(p1.X, p1.Y, x1, y1);
                p1.X = x1;
                p1.Y = y1;
            }

            else if (type == "2")
            {
                string point2 = tokens[2];
                string[] pts2 = point2.Split(',');

                int x = int.Parse(pts2[0]);
                int y = int.Parse(pts2[1]);

                p1.X = x;
                p1.Y = y;

            }
            else if (type == "3")
            {
                int x = int.Parse(tokens[1]);
                switch (x)
                {
                    case 0: clr = Color.Black; break;
                    case 1: clr = Color.Red; break;
                    case 2: clr = Color.Blue; break;
                    case 3: clr = Color.Green; break;
                    case 4: clr = Color.Yellow; break;
                    case 5: clr = Color.White; p_width = 20; trackBar1.Value = 20; break;
                    case 6: picBox.Image = null; break;
                }
            }
            else if (type == "4")
            {
                int v = int.Parse(tokens[1]);
                p_width = v;
            }
            else if (type == "5")
            {
                timer.Text = tokens[1];
                //road = int.Parse(tokens[2]);
            }


            // 클라이언트에선 데이터를 전달해줄 필요가 없으므로 바로 수신 대기한다.
            // 데이터를 받은 후엔 다시 버퍼를 비워주고 같은 방법으로 수신을 대기한다.
            obj.ClearBuffer();

            // 수신 대기
            obj.WorkingSocket.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
        }

        private void SendData(object sender, EventArgs e)
        {
            // 서버가 대기중인지 확인한다.
            if (!mainSock.IsBound)
            {
                MessageBox.Show("서버가 실행되고 있지 않습니다!");
                return;
            }

            // 보낼 텍스트
            string tts = txtTTS.Text.Trim();
            if (string.IsNullOrEmpty(tts))
            {
                MessageBox.Show("텍스트가 입력되지 않았습니다!");
                txtTTS.Focus();
                return;
            }

            // 서버 ip 주소와 메세지를 담도록 만든다.
            IPEndPoint ip = (IPEndPoint)mainSock.LocalEndPoint;
            string addr = ip.Address.ToString();
            string type = "0";
            // 문자열을 utf8 형식의 바이트로 변환한다.
            byte[] bDts = Encoding.UTF8.GetBytes(type + '\x01' + nick + '\x01' + tts);

            // 서버에 전송한다.
            mainSock.Send(bDts);

            // 전송 완료 후 텍스트박스에 추가하고, 원래의 내용은 지운다.
            AppendText(txtChat, string.Format("[보냄]{0}: {1}", nick, tts));
            txtTTS.Clear();

        }

        private void picBox_MouseDown(object sender, MouseEventArgs e)
        {
            p1.X = e.X;
            p1.Y = e.Y;

            string type = "2";
            pp1 = p2.X.ToString() + "," + p2.Y.ToString();
            pp2 = p1.X.ToString() + "," + p1.Y.ToString();

            byte[] bDts = Encoding.UTF8.GetBytes(type + '\x01' + "Ddd" + '\x01' + pp2);
            mainSock.Send(bDts);

        }

        private void picBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                p2.X = e.X;
                p2.Y = e.Y;

                string type = "1";
                pp1 = p2.X.ToString() + "," + p2.Y.ToString();
                pp2 = p1.X.ToString() + "," + p1.Y.ToString();
                byte[] bDts = Encoding.UTF8.GetBytes(type + '\x01' + pp1 + '\x01' + pp2);
                mainSock.Send(bDts);

                Draw(p1.X, p1.Y, p2.X, p2.Y);

                p1.X = e.X;
                p1.Y = e.Y;
            }
        }

        private void txtTTS_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendData(sender, e);
            }

        }

        private void Draw(float px1, float py1, float px2, float py2)
        {
            Graphics g = picBox.CreateGraphics();
            Pen a = new Pen(clr, p_width);

            a.EndCap = LineCap.Round;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            g.DrawLine(a, px1, py1, px2, py2);

            g.Dispose();
            a.Dispose();
        }

        private void blackB_Click(object sender, EventArgs e)
        {
            clr = Color.Black;
            color_send(0);
        }

        private void redB_Click(object sender, EventArgs e)
        {
            clr = Color.Red;
            color_send(1);
        }

        private void blueB_Click(object sender, EventArgs e)
        {
            clr = Color.Blue;
            color_send(2);
        }

        private void greenB_Click(object sender, EventArgs e)
        {
            clr = Color.Green;
            color_send(3);
        }

        private void yellowB_Click(object sender, EventArgs e)
        {
            clr = Color.Yellow;
            color_send(4);
        }

        private void whiteB_Click(object sender, EventArgs e)
        {
            clr = Color.White;
            p_width = 20;
            trackBar1.Value = 20;
            color_send(5);

        }

        private void deleteB_Click(object sender, EventArgs e)
        {
            picBox.Image = null;
            color_send(6);
        }

        private void giveupB_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = "굵기 : ";
            label1.Text += trackBar1.Value.ToString();
            p_width = trackBar1.Value;
            string type = "4";

            byte[] bDts = Encoding.UTF8.GetBytes(type + '\x01' + trackBar1.Value);
            mainSock.Send(bDts);
        }

        private void color_send(int color)
        {
            string type = "3";

            byte[] bDts = Encoding.UTF8.GetBytes(type + '\x01' + color);
            mainSock.Send(bDts);
        }

    }

    
}