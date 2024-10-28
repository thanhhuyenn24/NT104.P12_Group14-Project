using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{

    public partial class ServerForm : Form
    {
        private static Socket serverSocket;
        private Socket client;
        private static Thread clientThread;
        private static Thread serverlisten;
        private static List<Player> connectedPlayers = new List<Player>();
        public ServerForm()
        {
            InitializeComponent();
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverEP = new IPEndPoint(ipAddress, 11000);
            serverSocket.Bind(serverEP);
            serverSocket.Listen(3);
            richTextBox1.Text += "Chờ đợi kết nối từ người chơi ... \r\n";
        }
        public void recvfromClientsocket(Socket client)
        {

            Player player = new Player();
            player.playerSocket = client;
            connectedPlayers.Add(player);
            byte[] buffer = new byte[4096];

            while (player.playerSocket.Connected)
            {
                if (player.playerSocket.Available > 0)
                {
                    string message = "";
                    while (player.playerSocket.Available > 0)
                    {
                        int ReadfromBuffer = player.playerSocket.Receive(buffer);
                        message += Encoding.UTF8.GetString(buffer, 0, ReadfromBuffer);
                    }
                    UpdateRichTextBox(player.playerSocket.RemoteEndPoint + ": " + message); // Thêm dòng này để cập nhật RichTextBox
                    AnalyzingMess(message, player);
                }
            }

        }
        public static void AnalyzingMess(string mess, Player p)
        {
            string[] arrPayload = mess.Split(';');

            switch (arrPayload[0])
            {
                case "CONNECT":
                    {
                        string playerName = arrPayload[1].Trim();

                        // Kiểm tra tên đã tồn tại
                        if (connectedPlayers.Any(player => player.name == playerName))
                        {
                            byte[] errorBuffer = Encoding.UTF8.GetBytes("ERROR;Username already exists");
                            p.playerSocket.Send(errorBuffer);
                            return;
                        }

                        p.name = playerName;

                        // Gửi thông tin người chơi hiện tại cho người mới
                        foreach (var player in connectedPlayers)
                        {
                            if (player.name != null)
                            {
                                byte[] buffer = Encoding.UTF8.GetBytes($"LOBBYINFO;{player.name}");
                                p.playerSocket.Send(buffer);
                                Thread.Sleep(100);
                            }
                        }

                        // Thông báo cho các người chơi khác về người chơi mới
                        foreach (var player in connectedPlayers)
                        {
                            if (player.playerSocket != p.playerSocket && player.name != null)
                            {
                                byte[] buffer = Encoding.UTF8.GetBytes($"LOBBYINFO;{p.name}");
                                player.playerSocket.Send(buffer);
                                Thread.Sleep(100);
                            }
                        }
                    }
                    break;
            }
        }
        private void UpdateRichTextBox(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateRichTextBox), message);
                return;
            }

            richTextBox1.Text += message + Environment.NewLine;
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            serverlisten = new Thread(() =>
            {
                while (true)
                {
                    client = serverSocket.Accept();
                    UpdateRichTextBox("New connection from " + client.RemoteEndPoint);
                    clientThread = new Thread(() => recvfromClientsocket(client));
                    clientThread.Start();
                }
            });
            serverlisten.Start();
        }
    }
}