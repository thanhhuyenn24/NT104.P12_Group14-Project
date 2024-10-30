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
        public static string drawTime = "50";
        public static string rounds = "2";
        public static string wordCount = "2";
        private Timer turnTimer;
        private int timeLeft;
        private readonly object timerLock = new object();
        private bool isTimerRunning = false;
        public ServerForm()
        {
            InitializeComponent();
            InitializeServer();
            InitializeTimer();
        }
        private void InitializeServer()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverEP = new IPEndPoint(ipAddress, 11000);
            serverSocket.Bind(serverEP);
            serverSocket.Listen(3);
            richTextBox1.Text += "Chờ đợi kết nối từ người chơi ... \r\n";
        }
        private void InitializeTimer()
        {
            // Tạo timer trong UI thread
            turnTimer = new Timer();
            turnTimer.Interval = 1000; // 1 giây
            turnTimer.Tick += new EventHandler(TurnTimer_Tick);
        }

        private void TurnTimer_Tick(object sender, EventArgs e)
        {
            // Đảm bảo chỉ có một thread được cập nhật timer tại một thời điểm
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
                    currentturn++;
                    if (currentturn > 3)
                        currentturn = 1;
                    foreach (var player in connectedPlayers)
                    {
                        byte[] buffer = Encoding.UTF8.GetBytes("TURN;" + connectedPlayers[currentturn - 1].name);
                        player.playerSocket.Send(buffer);
                    }
                    Reset_Timer();
                }
            }
        }

        private void SafeUpdateTimerDisplay(int time)
        {
            // Cập nhật UI an toàn
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

        private void BroadcastTimeToClients(int time)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes($"TIME_CHANGE;{time}");
                foreach (var player in connectedPlayers)
                {
                    player.playerSocket.Send(buffer);
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
        public void AnalyzingMess(string mess, Player p)
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

                        // Kiểm tra nếu phòng đã đầy
                        if (connectedPlayers.Count > maxPlayers)
                        {
                            // Xử lý ngắt kết nối
                            connectedPlayers.Remove(p); // Loại bỏ player khỏi danh sách
                            richTextBox1.Text += ($"{p.name} has disconnected.\n"); // Cập nhật log cho server
                            return; // Kết thúc xử lý cho client này
                        }

                        // Thông báo cho các người chơi khác về người chơi mới
                        foreach (var player in connectedPlayers)
                        {
                            string makemsg = "LOBBYINFO;" + p.name + ";" + p.turn + ";" + connectedPlayers.Count;
                            byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                            player.playerSocket.Send(buffer);
                            Thread.Sleep(100);

                        }
                    }
                    break;
                case "DISCONNECT":
                    {
                        // Xử lý ngắt kết nối
                        connectedPlayers.Remove(p); // Loại bỏ player khỏi danh sách
                        richTextBox1.Text +=($"{p.name} has disconnected.\n"); // Cập nhật log cho server
                        // Thông báo cho tất cả người chơi còn lại
                        foreach (var player in connectedPlayers)
                        {
                            byte[] buffer = Encoding.UTF8.GetBytes($"DISCONNECT;{p.name} has left the game.\n");
                            player.playerSocket.Send(buffer);
                        }
                        // Đóng socket của player
                        p.playerSocket.Close();
                    }
                    break;
                case "UPDATE_SETTINGS":
                    {
                        maxPlayers = int.Parse(arrPayload[1]);
                        players = arrPayload[1];
                        drawTime = arrPayload[2];
                        rounds = arrPayload[3];
                        wordCount = arrPayload[4];

                        string updateMessage = "UPDATE_SETTINGS;" + players + ";" + drawTime + ";" + rounds + ";" + wordCount + ";" + connectedPlayers.Count;

                        // Gửi thông báo cập nhật đến tất cả các client
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
                            RandomWords();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Vui lòng chọn file gói từ trước khi bắt đầu trò chơi !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        foreach (var player in connectedPlayers)
                        {
                            string makemsg = "LOAD_WORD;" + word;
                            byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                            player.playerSocket.Send(buffer);
                            Thread.Sleep(100);
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
                                    Thread.Sleep(100);
                                }
                            }
                        }

                        foreach (var player in connectedPlayers)
                        {
                            string makemsg = "SETUP;" + player.name;
                            byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                            player.playerSocket.Send(buffer);
                            Console.WriteLine("Sendback: " + makemsg);
                            Thread.Sleep(100);
                        }
                        Reset_Timer();
                        foreach (var player in connectedPlayers)
                        {
                            string makemsg_ = "TURN;" + connectedPlayers[currentturn - 1].name;
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
                                byte[] buffer = Encoding.UTF8.GetBytes("PIC_UPDATE;" + arrPayload[1]); //CR + character
                                player.playerSocket.Send(buffer);
                            }
                        }
                    }
                    break;

                case "GUESS_RIGHT":
                    {
                        foreach (var player in connectedPlayers)
                        {
                            if (player.playerSocket != p.playerSocket)
                            {
                                byte[] buffer = Encoding.UTF8.GetBytes("CR;" + arrPayload[1]); //CR + character
                                player.playerSocket.Send(buffer);
                            }
                        }
                    }
                    break;
                case "GUESS_WRONG":
                    {
                        currentturn++;
                        if (currentturn > 3)
                            currentturn = 1;
                        foreach (var player in connectedPlayers)
                        {
                            byte[] buffer = Encoding.UTF8.GetBytes("CW;" + arrPayload[1]); //CW + character
                            player.playerSocket.Send(buffer);
                            Thread.Sleep(100);
                            buffer = Encoding.UTF8.GetBytes("TURN;" + connectedPlayers[currentturn - 1].name);
                            player.playerSocket.Send(buffer);
                        }
                    }
                    break;

                case "SCORE_CHANGED":
                    {
                        foreach (var player in connectedPlayers)
                        {
                            // Update Name + Score
                            byte[] buffer = Encoding.UTF8.GetBytes("SCORE_UPDATE;" + arrPayload[1] + ";" + arrPayload[2]);
                            player.playerSocket.Send(buffer);
                        }
                    }
                    break;
                default:
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

        private static void SetUpPlayerTurn()
        {
            int i = 1;
            foreach (var player in connectedPlayers)
            {
                player.turn = i;
                i++;
            }
        }
        private void SendCurrentSettingsToClient(Player player)
        {
            // Gửi thông tin cài đặt hiện tại cho client mới
            string settingsMessage = "UPDATE_SETTINGS;" + players + ";" + drawTime + ";" + rounds + ";" + wordCount + ";" + connectedPlayers.Count;
            byte[] buffer = Encoding.UTF8.GetBytes(settingsMessage);
            player.playerSocket.Send(buffer);
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            serverSocket.Close();
            serverlisten.Abort();
            if (clientThread != null)
                clientThread.Abort();
        }

        #region JSON
        public static void RandomWords()
        {
            // Đọc nội dung từ tệp JSON
            string jsonText = File.ReadAllText(wordPath);

            // Phân tích nội dung JSON
            JObject json = JObject.Parse(jsonText);

            // Lấy ra mảng các từ
            JArray wordsArray = (JArray)json["words"];

            // Chọn ngẫu nhiên một từ từ mảng
            Random random = new Random();
            int randomIndex = random.Next(0, wordsArray.Count);
            string word = (string)wordsArray[randomIndex]["word"];

            // Kiểm tra từ đã được sử dụng hay chưa
            while (UsedWords.Contains(word))
            {
                randomIndex = random.Next(0, wordsArray.Count);
                word = (string)wordsArray[randomIndex]["word"];
            }

            // Thêm từ vào danh sách đã sử dụng
            UsedWords.Add(word);
        }

        private void btnLoadWords_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Đường dẫn của file được chọn
                MessageBox.Show("Nạp từ thành công !", "Thông báo", MessageBoxButtons.OK);
                wordPath = openFileDialog.FileName;

            }
        }

        #endregion

    }
}