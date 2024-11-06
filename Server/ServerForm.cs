using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using Timer = System.Windows.Forms.Timer;

namespace Server
{

    public partial class ServerForm : Form
    {
        private static Socket serverSocket;
        private Socket client;
        private static Thread clientThread;
        private static Thread serverlisten;
        private static List<Player> connectedPlayers = new List<Player>();
        private static List<string> UsedWords = new List<string>();
        private static int currentturn = 1;
        private static int currentround = 1;
        private static string word;
        private static string wordPath;
        public static int maxPlayers = 2;
        public static string players = "2";
        public static string drawTime = "30";
        public static string rounds = "1";
        private Timer turnTimer;
        private int timeLeft;
        private readonly object timerLock = new object();
        private bool isTimerRunning = false;
        public ServerForm()
        {
            InitializeComponent();
            InitializeServer();
            InitializeTimer();

            btnLoadWords.FlatStyle = FlatStyle.Flat;
            btnLoadWords.BackColor = Color.Transparent;
            btnLoadWords.FlatAppearance.BorderSize = 0;
            btnLoadWords.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnLoadWords.FlatAppearance.MouseDownBackColor = Color.Transparent;

            lbTime.FlatStyle = FlatStyle.Flat;
            lbTime.BackColor = Color.Transparent;
        }
        
        #region TIMER
        private void InitializeTimer()
        {
            //Tao Timer
            turnTimer = new Timer();
            turnTimer.Interval = 1000; //1 giay
            turnTimer.Tick += new EventHandler(TurnTimer_Tick);
        }

        private void TurnTimer_Tick(object sender, EventArgs e)
        {
            //Dam bao chi co 1 thread duoc cap nhat timer trong 1 thoi diem
            lock (timerLock)
            {
                if (!isTimerRunning) return;

                if (timeLeft > 0)
                {
                    timeLeft--;
                    SafeUpdateTimerDisplay(timeLeft);
                    BroadcastTimeToClients(timeLeft);
                }
                else
                {
                    StopTimer();

                    foreach (var player in connectedPlayers)
                    {
                        string makemsg = "CLEAR_PIC;";
                        byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                        player.playerSocket.Send(buffer);
                        Console.WriteLine("Sendback: " + makemsg);
                        Thread.Sleep(100);
                    }
                    string makemsg_ = "TIME_OUT;";
                    makemsg_ += connectedPlayers.Count();
                    foreach (var player in connectedPlayers)
                    {
                        makemsg_ += ";" + player.name + ";" + player.score.ToString();
                    }
                    foreach (var player in connectedPlayers)
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes(makemsg_);
                        player.playerSocket.Send(buffer);
                        Console.WriteLine("Sendback: " + makemsg_);
                        Thread.Sleep(100);
                    }
                    currentturn++;
                    if (currentturn > maxPlayers)
                    {
                        currentturn = 1;
                        currentround++;
                        foreach (var player in connectedPlayers)
                        {
                            string roundmsg = "ROUND_CHANGE;" + currentround.ToString();
                            byte[] buffer = Encoding.UTF8.GetBytes(roundmsg);
                            player.playerSocket.Send(buffer);
                            Console.WriteLine("Sendback: " + roundmsg);
                            Thread.Sleep(100);
                        }
                        //Kiem tra neu vuot qua so vong choi
                        if (currentround > int.Parse(rounds))
                        {
                            connectedPlayers.Sort((x, y) => x.score.CompareTo(y.score));
                            string WinnerName = connectedPlayers[connectedPlayers.Count - 1].name;
                            foreach (var player in connectedPlayers)
                            {
                                string makemsg = "ENDGAME;" + WinnerName;
                                byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                                player.playerSocket.Send(buffer);
                                Console.WriteLine("Sendback: " + makemsg);
                                Thread.Sleep(100);
                            }
                            return; //Ket thuc va khong gui them thong diep nao nua
                        }
                    }
                    word = RandomWords();
                    foreach (var player in connectedPlayers)
                    {
                        string makemsg = "TURN;" + connectedPlayers[currentturn - 1].name + ";" + word;
                        byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                        player.playerSocket.Send(buffer);
                        Console.WriteLine("Sendback: " + makemsg);
                        Thread.Sleep(100);
                    }
                    Reset_Timer();
                }
            }
        }

        private void SafeUpdateTimerDisplay(int time)
        {
            if (lbTime.InvokeRequired)
            {
                lbTime.Invoke(new Action(() => {
                    lbTime.Text = time.ToString();
                }));
            }
            else
            {
                lbTime.Text = time.ToString();
            }
        }

        //Gui thoi gian dem nguoc cho client
        private void BroadcastTimeToClients(int time)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes($"TIME_CHANGE;{time}");
                foreach (var player in connectedPlayers)
                {
                    player.playerSocket.Send(buffer);
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error broadcasting time: {ex.Message}");
            }
        }

        public void Reset_Timer()
        {
            lock (timerLock)
            {
                StopTimer();
                timeLeft = int.Parse(drawTime);
                SafeUpdateTimerDisplay(timeLeft);
                StartTimer();
            }
        }

        private void StartTimer()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => {
                    isTimerRunning = true;
                    turnTimer.Start();
                }));
            }
            else
            {
                isTimerRunning = true;
                turnTimer.Start();
            }
        }

        private void StopTimer()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => {
                    isTimerRunning = false;
                    turnTimer.Stop();
                }));
            }
            else
            {
                isTimerRunning = false;
                turnTimer.Stop();
            }
        }
        #endregion

        #region NETWORK(Connect, read data)
        private void InitializeServer()
        {
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
            // Gọi hàm thiết lập turn tại đây
            SetUpPlayerTurn(); // Thiết lập turn sau khi thêm người chơi

            // Gửi thông tin cài đặt hiện tại cho client mới
            if (connectedPlayers.Count!=1) SendCurrentSettingsToClient(player);

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
        #endregion

        //Phan tich thong diep tu client
        public void AnalyzingMess(string mess, Player p)
        {
            string[] arrPayload = mess.Split(';');

            switch (arrPayload[0])
            {
                case "CONNECT":
                    {
                        string playerName = arrPayload[1].Trim();

                        p.name = playerName;

                        //Kiem tra phong da day
                        if (connectedPlayers.Count > maxPlayers)
                        {
                            //Xu li ngat ket noi
                            connectedPlayers.Remove(p); // Loai bo player khoi danh sach
                            richTextBox1.Text += ($"{p.name} has disconnected.\n");
                            return; //Ket thuc xu li cho client nay
                        }

                        //Kiem tra ten da ton tai
                        if (connectedPlayers.Any(player =>
                        player != p &&
                        player.name?.Equals(playerName, StringComparison.OrdinalIgnoreCase) == true))
                        {
                            connectedPlayers.Remove(p);
                            return;
                        }

                        //Thong bao cho cac nguoi choi  khac ve nguoi choi moi
                        foreach (var player in connectedPlayers)
                        {
                            string makemsg = "LOBBYINFO;" + p.name + ";" + p.turn + ";" + connectedPlayers.Count;
                            byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                            player.playerSocket.Send(buffer);
                            Thread.Sleep(150);

                        }
                    }
                    break;
                case "DISCONNECT":
                    {
                        //Xu ki ngat ket noi
                        connectedPlayers.Remove(p); //Loai bo player khoi danh sach
                        richTextBox1.Text +=($"{p.name} has disconnected.\n");
                        //Thong bao cho tat ca nguoi choi con lai
                        foreach (var player in connectedPlayers)
                        {
                            byte[] buffer = Encoding.UTF8.GetBytes($"DISCONNECT;{p.name} has left the game.\n");
                            player.playerSocket.Send(buffer);
                        }
                        //Dong socket cua player
                        p.playerSocket.Close();
                    }
                    break;
                case "UPDATE_SETTINGS":
                    {
                        maxPlayers = int.Parse(arrPayload[1]);
                        players = arrPayload[1];
                        drawTime = arrPayload[2];
                        rounds = arrPayload[3];

                        string updateMessage = "UPDATE_SETTINGS;" + players + ";" + drawTime + ";" + rounds + ";" + connectedPlayers.Count;

                        //Gui thong tin setting da cap nhat cho cac client
                        foreach (var player in connectedPlayers)
                        {
                            if (player.playerSocket.Connected)
                            {
                                byte[] buffer = Encoding.UTF8.GetBytes(updateMessage);
                                player.playerSocket.Send(buffer);
                                Thread.Sleep(100);
                            }
                        }
                    }
                    break;
                case "START":
                    {
                        try
                        {
                            word = RandomWords();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Vui lòng chọn file gói từ trước khi bắt đầu trò chơi !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        SetUpPlayerTurn();
                        connectedPlayers.Sort((x, y) => x.turn.CompareTo(y.turn));
                        foreach (var player in connectedPlayers)
                        {
                            string makemsg = "INGAME;" + player.name + ";" + player.turn + ";" + player.score;
                            byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                            player.playerSocket.Send(buffer);
                            Thread.Sleep(100);
                        }


                        foreach (var player in connectedPlayers)
                        {
                            foreach (var player_ in connectedPlayers)
                            {
                                if (player.name != player_.name)
                                {
                                    string makemsg = "OTHERINFO;" + player_.name + ";" + player_.turn + ";" + player.score;
                                    byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                                    player.playerSocket.Send(buffer);
                                    Console.WriteLine("Sendback: " + makemsg);
                                    Thread.Sleep(150);
                                }
                            }
                        }

                        foreach (var player in connectedPlayers)
                        {
                            string makemsg = "SETUP;" + player.name + ";" + drawTime; //Gui them drawTime cho WordHint
                            byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                            player.playerSocket.Send(buffer);
                            Console.WriteLine("Sendback: " + makemsg);
                            Thread.Sleep(150);
                        }
                        Reset_Timer();
                        foreach (var player in connectedPlayers)
                        {
                            string makemsg_ = "TURN;" + connectedPlayers[currentturn - 1].name + ";" + word;
                            byte[] buffer_ = Encoding.UTF8.GetBytes(makemsg_);
                            player.playerSocket.Send(buffer_);
                        }
                    }
                    break;
                case "PIC_CHANGE":
                    {
                        foreach (var player in connectedPlayers)
                        {
                            if (player.playerSocket != p.playerSocket)
                            {
                                byte[] buffer = Encoding.UTF8.GetBytes("PIC_UPDATE;" + arrPayload[1]);
                                player.playerSocket.Send(buffer);
                            }
                        }
                    }
                    break;

                case "GUESS_RIGHT":
                    {
                        
                        foreach (var player in connectedPlayers)
                        {
                            byte[] buffer = Encoding.UTF8.GetBytes("GR;" + arrPayload[1] + ";" + connectedPlayers[currentturn-1].name); //name + name nguoi ve
                            player.playerSocket.Send(buffer);
                            Thread.Sleep(100);
                        }
                    }
                    break;
                case "UPDATE":
                    {
                        foreach (var player in connectedPlayers)
                        {
                            if (player.name == arrPayload[1])
                            {
                                player.score = int.Parse(arrPayload[2]);
                                Thread.Sleep(100);
                            }
                        }
                    }
                    break;
                case "GUESS_WRONG":
                    {
                        foreach (var player in connectedPlayers)
                        {
                            byte[] buffer = Encoding.UTF8.GetBytes("GW;" + arrPayload[1] + ";" + arrPayload[2]); //name + word
                            player.playerSocket.Send(buffer);
                            Thread.Sleep(100);
                        }
                    }
                    break;
                default:
                    break;

            }
        }

        #region Other Functions

        //Update thong diep client gui cho server
        private void UpdateRichTextBox(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateRichTextBox), message);
                return;
            }

            richTextBox1.Text += message + Environment.NewLine;
        }


        //Setup luot choi
        private static void SetUpPlayerTurn()
        {
            int i = 1;
            foreach (var player in connectedPlayers)
            {
                player.turn = i;
                i++;
            }
        }

        //Gui thong tin cai dat hien tai cho client moi
        private void SendCurrentSettingsToClient(Player player)
        {
            string settingsMessage = "UPDATE_SETTINGS;" + players + ";" + drawTime + ";" + rounds + ";" + connectedPlayers.Count;
            byte[] buffer = Encoding.UTF8.GetBytes(settingsMessage);
            player.playerSocket.Send(buffer);
        }

        //Ngat ket noi khi dong server form
        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            serverSocket.Close();
            serverlisten.Abort();
            if (clientThread != null)
                clientThread.Abort();
        }
        #endregion

        #region JSON
        public static string RandomWords()
        {
            //Doc noi dung tu JSON
            string jsonText = File.ReadAllText(wordPath);

            //Phan tich JSON
            JObject json = JObject.Parse(jsonText);

            //Lay ra mang cac tu
            JArray wordsArray = (JArray)json["words"];

            //Chon ngau nhien 1 tu trong mang
            Random random = new Random();
            int randomIndex = random.Next(0, wordsArray.Count);
            string word = (string)wordsArray[randomIndex]["word"];

            //Kiem tra tu da duoc su dung chua
            while (UsedWords.Contains(word))
            {
                randomIndex = random.Next(0, wordsArray.Count);
                word = (string)wordsArray[randomIndex]["word"];
            }

            //Them tu vao danh sach da su dung
            UsedWords.Add(word);
            return word;
        }

        private void btnLoadWords_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Duong dan file duoc chon
                MessageBox.Show("Nạp từ thành công !", "Thông báo", MessageBoxButtons.OK);
                wordPath = openFileDialog.FileName;

            }
        }

        #endregion

    }
}