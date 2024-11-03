using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client
{

    public partial class GiaoDienTaoPhong : Form
    {
        public static GiaoDienTaoPhong lobby;
        public int connectedPlayer = 0;
        public static GiaoDienTaoPhong Instance { get; private set; }

        List<Player> players = new List<Player>();

        public int getPlayers()
        {
            return int.Parse(this.comboBoxPlayers.Text);
        }

        public GiaoDienTaoPhong()
        {
            InitializeComponent();
            //Khoa kiem tra luong de khong bi Cross-thread
            CheckForIllegalCrossThreadCalls = false;
            lobby = this;
            comboBoxPlayers.SelectedIndex = 0;
            comboBoxDrawTime.SelectedIndex = 0;
            comboBoxRounds.SelectedIndex = 0;

            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.BackColor = Color.Transparent;
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnOK.FlatAppearance.MouseDownBackColor = Color.Transparent;

            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.BackColor = Color.Transparent;
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnStart.FlatAppearance.MouseDownBackColor = Color.Transparent;

            btnLeave.FlatStyle = FlatStyle.Flat;
            btnLeave.BackColor = Color.Transparent;
            btnLeave.FlatAppearance.BorderSize = 0;
            btnLeave.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnLeave.FlatAppearance.MouseDownBackColor = Color.Transparent;
        }

        //Du so luong nguoi choi moi duoc Start
        public void Disable_Enable_Start(bool check)
        {
            if (check == true)
                btnStart.Enabled = true;
            else btnStart.Enabled = false;
        }

        //Chi nguoi vao phong dau tien duoc cai dat
        public void Enable_All()
        {
            comboBoxPlayers.Enabled = true;
            comboBoxDrawTime.Enabled = true;
            comboBoxRounds.Enabled = true;
            btnOK.Enabled = true;
        }

        public void UpdateSettings(string players, string drawTime, string rounds)
        {
            comboBoxPlayers.Text = players;
            comboBoxDrawTime.Text = drawTime;
            comboBoxRounds.Text = rounds;
        }

        public void UpdatePlayerCount(int playerCount)
        {
            Status.Text = $"Số lượng người chơi đã vào: {playerCount}\n";
        }

        public void UpdateDisconnect(string sentence)
        {
            Status.Text += $"{sentence}";
        }

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            Client_Socket.datatype = "UPDATE_SETTINGS";
            string settings = comboBoxPlayers.Text + ";" + comboBoxDrawTime.Text + ";" + comboBoxRounds.Text;

            //Gui thong tin cai dat toi server
            Client_Socket.SendMessage(settings);
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            try
            {
                //Gui thong diep ngat ket noi toi server
                Client_Socket.datatype = "DISCONNECT";
                Client_Socket.SendMessage(Player.name);
                //Ngat ket noi socket
                Client_Socket.clientSocket.Shutdown(SocketShutdown.Both);
                Client_Socket.clientSocket.Close();
                //Dong form sanh cho
                GiaoDienChinh.lobby.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi ngắt kết nối: " + ex.Message);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Client_Socket.datatype = "START";
            Client_Socket.SendMessage(" ");
        }
    }
}