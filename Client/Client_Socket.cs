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
using System.Drawing;
using System.IO;


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

        public static GiaoDienNguoiChoi GamePlay;
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
                                Payload[1], Payload[2], Payload[3]
                            );

                            int currentPlayers = int.Parse(Payload[4]);  // Số lượng người chơi hiện tại từ server

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
                        GamePlay = new GiaoDienNguoiChoi();
                        Player.turn = int.Parse(Payload[2]);
                        Player.score = int.Parse(Payload[3]);
                        otherPlayers = new List<OtherPlayers>();
                        GiaoDienChinh.lobby.Invoke((MethodInvoker)delegate ()
                        {
                            GamePlay.Text = Payload[1];
                            GamePlay.round = Playerround.ToString();
                            GamePlay.Show();
                            GiaoDienChinh.lobby.Hide();
                        }
                        );
                    }
                    break;
                case "OTHERINFO":
                    {
                        OtherPlayers otherplayer = new OtherPlayers();
                        otherplayer.name = Payload[1];
                        otherplayer.turn = Payload[2];
                        otherplayer.score = Payload[3];
                        otherPlayers.Add(otherplayer);
                    }
                    break;
                case "SETUP":
                    {
                        GamePlay.Invoke((MethodInvoker)delegate ()
                        {
                            GamePlay.InGameDisplay();
                        }
                        );
                    }
                    break;
                case "TIME_CHANGE":
                    {
                        GamePlay.labelTimer.Text = Payload[1];
                    }
                    break;
                case "TURN":
                    {
                        GamePlay.word = Payload[2];
                        if (Payload[1] == Player.name)
                        {
                            GamePlay.Invoke((MethodInvoker)delegate ()
                            {
                                GamePlay.Allow_Playing();
                                GamePlay.Turn_Notify(Payload[1]);
                            }
                            );
                        }
                        else
                        {
                            GamePlay.Invoke((MethodInvoker)delegate ()
                            {
                                GamePlay.NotAllowPlaying();
                                GamePlay.Turn_Notify(Payload[1]);
                            }
                            );
                        }
                    }
                    break;
                case "PIC_UPDATE":
                    {
                        Bitmap newBitmap;
                        newBitmap = StringToBitmap( Payload[1] );
                        GamePlay.pic.Image = newBitmap;
                        GamePlay.pic.Refresh();
                    }
                    break;
                case "CLEAR_PIC":
                    {
                        GamePlay.Clear_pic();
                    }
                    break;
                case "ROUND_CHANGE":
                    {
                        GamePlay.label2.Text = "Round: " + Payload[1];
                    }
                    break;
                case "GR":
                    {
                        if (Payload[1]==Player.name)
                        {
                            Player.score += 200;
                        }
                        if (Payload[2]==Player.name)
                        {
                            Player.score += 100;
                        }
                        GamePlay.Invoke((MethodInvoker)delegate ()
                        {
                            GamePlay.Game_Update_RIGHT(Payload[1]);
                        }
                        );
                        if (Payload[1] == Player.name)
                        {
                            GamePlay.Invoke((MethodInvoker)delegate ()
                            {
                                GamePlay.GR();
                            }
                        );
                        }
                        datatype = "UPDATE";
                        msg = Player.name + ";" +Player.score.ToString();
                        SendMessage(msg);
                        
                    }
                    break;
                case "TIME_OUT":
                    {
                        for (int i = 2; i < int.Parse(Payload[1])*2+2;)
                        {
                            GamePlay.Score_Update(Payload[i], Payload[i+1]);
                            i += 2;
                        }
                    }
                    break;
                case "UPDATE":
                    {
                        for (int i = 2; i < int.Parse(Payload[1]) * 2 + 2;)
                        {
                            GamePlay.Score_Update(Payload[i], Payload[i + 1]);
                            i += 2;
                        }
                    }
                    break;
                case "GW":
                    {
                        GamePlay.Invoke((MethodInvoker)delegate ()
                        {
                            GamePlay.Game_Update_WRONG(Payload[1], Payload[2]);
                        }
                       );
                    }
                    break;
                case "ENDGAME":
                    {
                        MessageBox.Show(Payload[1] + " is WIN");
                    }
                    break;
                default:
                    break;
            }

        }
        public static Bitmap StringToBitmap(string base64String)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(base64String);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    return new Bitmap(ms);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in StringToBitmap: {ex.Message}");
                return null;
            }
        }
    }
}