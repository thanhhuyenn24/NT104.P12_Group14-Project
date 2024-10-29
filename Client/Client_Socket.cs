using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;

namespace Client
{
    public class Client_Socket
    {
        public static Socket clientSocket;
        public static Thread recvThread;
        public static string datatype = ""; // Kieu du lieu nguoi choi gui cho server        
        public static int Playerround = 1;

        public static void Connect(IPEndPoint serverEP)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(serverEP);
            recvThread = new Thread(() => readingReturnData());
            recvThread.Start();
        }

        public static void SendMessage(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
                {
                    throw new ArgumentException("Dữ liệu gửi không được rỗng");
                }

                // Format message theo đúng protocol
                string msgstr = $"{datatype};{data}";

                // Kiểm tra kết nối trước khi gửi
                if (clientSocket == null || !clientSocket.Connected)
                {
                    throw new Exception("Không có kết nối tới server");
                }

                // Convert string to bytes và gửi
                byte[] msg = Encoding.UTF8.GetBytes(msgstr);
                int bytesSent = clientSocket.Send(msg);

                // Kiểm tra xem đã gửi đủ dữ liệu chưa
                if (bytesSent != msg.Length)
                {
                    throw new Exception("Gửi dữ liệu không thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gửi dữ liệu: {ex.Message}");
                throw;
            }
        }
        public static void readingReturnData()
        {
            byte[] buffer = new byte[1024];
            try
            {
                while (clientSocket.Connected)
                {
                    if (clientSocket.Available > 0)
                    {
                        string msg = "";
                        try
                        {
                            while (clientSocket.Available > 0)
                            {
                                int bRead = clientSocket.Receive(buffer);
                                if (bRead > 0)
                                {
                                    msg += Encoding.UTF8.GetString(buffer, 0, bRead);
                                }
                            }
                            if (!string.IsNullOrEmpty(msg))
                            {
                                AnalyzingReturnMessage(msg);
                            }
                        }
                        catch (SocketException se)
                        {
                            MessageBox.Show($"Lỗi khi nhận dữ liệu: {se.Message}");
                            break;
                        }
                    }
                    Thread.Sleep(100); // Tránh CPU quá tải
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối: {ex.Message}");
            }
            finally
            {
                if (clientSocket != null && clientSocket.Connected)
                {
                    clientSocket.Close();
                }
            }
        }
        //public static GiaoDienTaoPhong Lobby;
        public static GiaoDienNguoiChoi GamePlay;
        //public static Winner WinnerForm;
        public static List<OtherPlayers> otherPlayers;
        public static void AnalyzingReturnMessage(string msg)
        {
            string[] Payload = msg.Split(';');
            switch (Payload[0])
            {
                case "LOBBYINFO":
                    {

                        string playerName = Payload[1];
                        GiaoDienChinh.lobby.Invoke((MethodInvoker)delegate
                        {
                            int currentPlayers = int.Parse(Payload[3]);  // Số lượng người chơi hiện tại từ server

                            GiaoDienChinh.lobby.UpdatePlayerCount(currentPlayers);

                            //Kiem tra chu phong
                            if (int.Parse(Payload[2]) == 1)
                            {
                                GiaoDienChinh.lobby.Invoke((MethodInvoker)delegate
                                {
                                    GiaoDienChinh.lobby.Enable_All();
                                });
                            }

                            else
                            {

                            }

                            // Kiểm tra điều kiện kích hoạt nút Start
                            if (currentPlayers == GiaoDienChinh.lobby.getPlayers())
                                GiaoDienChinh.lobby.Disable_Enable_Start(true);
                            else
                                GiaoDienChinh.lobby.Disable_Enable_Start(false);

                        });
                        
                    }
                    break;
                case "DISCONNECT":
                    {
                        GiaoDienTaoPhong.lobby.Invoke((MethodInvoker)delegate
                        {
                            GiaoDienTaoPhong.lobby.UpdateDisconnect(Payload[1]
                            );
                        });
                        
                        if (Payload[1] == "Room is full") MessageBox.Show("Room is full!");
                    }
                    break;
                case "UPDATE_SETTINGS":
                    {
                        GiaoDienTaoPhong.lobby.Invoke((MethodInvoker)delegate
                        {
                            GiaoDienTaoPhong.lobby.UpdateSettings(
                                Payload[1], Payload[2], Payload[3], Payload[4]
                            );

                            int currentPlayers = int.Parse(Payload[5]);  // Số lượng người chơi hiện tại từ server

                            // Kiểm tra điều kiện kích hoạt nút Start
                            if (currentPlayers == GiaoDienChinh.lobby.getPlayers())
                                GiaoDienChinh.lobby.Disable_Enable_Start(true);
                            else
                                GiaoDienChinh.lobby.Disable_Enable_Start(false);
                        });
                    }
                    break;
                case "INGAME":
                    {
                        // Chuyển sang GiaoDienNguoiChoi
                        GiaoDienNguoiChoi GamePlay = new GiaoDienNguoiChoi();
                        GiaoDienChinh.lobby.Invoke((MethodInvoker)delegate ()
                        {
                            GamePlay.Show();
                            GiaoDienChinh.lobby.Hide();
                        }
                        );
                    }
                    break;
            }

        }
    }
}