﻿using System;
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
    public partial class Server : Form
    {
        delegate void AppendTextDelegate(Control ctrl, string s);
        AppendTextDelegate _textAppender;
        Socket mainSock;
        IPAddress thisAddress;
        Form1 f1;
        Client CClient;
        EndPoint road;
        int time = 0;
        bool start = false;

        public Server()
        {
            InitializeComponent();
            mainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _textAppender = new AppendTextDelegate(AppendText);
            f1 = (Form1)this.Owner;
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


        public bool RunServer(string txtIP, string txtPort)
        {
            
            int port;
            if (!int.TryParse(txtPort, out port))
            {
                
                MessageBox.Show("포트 번호가 잘못 입력되었거나 입력되지 않았습니다.");
                return false;
            }

            // 서버에서 클라이언트의 연결 요청을 대기하기 위해
            // 소켓을 열어둔다.
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(txtIP), port);
            mainSock.Bind(serverEP);
            mainSock.Listen(10);
            
            // 비동기적으로 클라이언트의 연결 요청을 받는다.
            mainSock.BeginAccept(AcceptCallback, null);
            return true;
        }


        List<Socket> connectedClients = new List<Socket>();
        void AcceptCallback(IAsyncResult ar)
        {
            // 클라이언트의 연결 요청을 수락한다.
            Socket client = mainSock.EndAccept(ar);

            // 또 다른 클라이언트의 연결을 대기한다.
            mainSock.BeginAccept(AcceptCallback, null);

            AsyncObj obj = new AsyncObj(4096);
            obj.WorkingSocket = client;

            // 연결된 클라이언트 리스트에 추가해준다.
            connectedClients.Add(client);

            // 텍스트박스에 클라이언트가 연결되었다고 써준다.
            AppendText(txtChat, string.Format("클라이언트 (@ {0})가 연결되었습니다.", client.RemoteEndPoint));
            //IPAddress asdf = IPAddress.Parse((string)client.RemoteEndPoint);
            // 클라이언트의 데이터를 받는다.
            client.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
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

            // 0x01 기준으로 짜른다.
            // tokens[0] - 보낸 사람 IP
            // tokens[1] - 보낸 메세지
            string[] tokens = text.Split('\x01');
            string ip = tokens[0];
            string msg = tokens[1];

            // 텍스트박스에 추가해준다.
            // 비동기식으로 작업하기 때문에 폼의 UI 스레드에서 작업을 해줘야 한다.
            // 따라서 대리자를 통해 처리한다.
            // AppendText(txtChat, string.Format("[받음]{0}: {1}", ip, msg));

            // for을 통해 "역순"으로 클라이언트에게 데이터를 보낸다.
            for (int i = connectedClients.Count - 1; i >= 0; i--)
            {
                if (connectedClients[i].RemoteEndPoint != road || ip == "0" || ip == "5")
                    break;

                Socket socket = connectedClients[i];
                if (socket != obj.WorkingSocket)
                {
                    try { socket.Send(obj.Buffer); }
                    catch
                    {
                        // 오류 발생하면 전송 취소하고 리스트에서 삭제한다.
                        try { socket.Dispose(); }
                        catch { }
                        connectedClients.RemoveAt(i);
                    }
                }
            }

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
            string tts = CClient.txtTTS.Text.Trim();
            if (string.IsNullOrEmpty(tts))
            {
                MessageBox.Show("텍스트가 입력되지 않았습니다!");
                CClient.txtTTS.Focus();
                return;
            }

            // 문자열을 utf8 형식의 바이트로 변환한다.
            byte[] bDts = Encoding.UTF8.GetBytes(thisAddress.ToString() + '\x01' + tts);

            // 연결된 모든 클라이언트에게 전송한다.
            for (int i = connectedClients.Count - 1; i >= 0; i--)
            {
                Socket socket = connectedClients[i];
                try { socket.Send(bDts); }
                catch
                {
                    // 오류 발생하면 전송 취소하고 리스트에서 삭제한다.
                    try { socket.Dispose(); }
                    catch { }
                    connectedClients.RemoveAt(i);
                }
            }

            // 전송 완료 후 텍스트박스에 추가하고, 원래의 내용은 지운다.
            //AppendText(txtChat, string.Format("[보냄]{0}: {1}", thisAddress.ToString(), tts));
            CClient.txtTTS.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            String type = "5";
            byte[] bDts;

            //if (time >= 60)
            //{
            //    road = connectedClients[Int32.Parse("asdasd") % connectedClients.Count].RemoteEndPoint;
            //    time %= 60;
            //    start = true;
            //}
            //if (start)
            //{
            //    int tmp = 60 - time;
            //    bDts = Encoding.UTF8.GetBytes(type + '\x01' + tmp + '\x01' + road);
            //}
            //else
            //{
            //    int tmp = 60 - time;
            //    bDts = Encoding.UTF8.GetBytes(type + '\x01' + tmp + '\x01' + "123");
            //}
            if (time >= 60)
                time %= 60;
            int tmp = 60 - time;
            bDts = Encoding.UTF8.GetBytes(type + '\x01' + tmp);
            // 연결된 모든 클라이언트에게 전송한다.
            for (int i = 0; i < connectedClients.Count ; i++)
            {
                Socket socket = connectedClients[i];
                try { socket.Send(bDts); }
                catch
                {
                    // 오류 발생하면 전송 취소하고 리스트에서 삭제한다.
                    try { socket.Dispose(); }
                    catch { }
                    connectedClients.RemoveAt(i);
                }
            
            }
        }


    }
}