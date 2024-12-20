﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client
{
    public partial class GiaoDienChinh : Form
    {
        public static GiaoDienTaoPhong lobby;
        public GiaoDienChinh()
        {
            InitializeComponent();

            //Thiet lap nut btnPlay
            btnPlay.FlatStyle = FlatStyle.Flat;
            btnPlay.BackColor = Color.Transparent;
            btnPlay.FlatAppearance.BorderSize = 0;
            btnPlay.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnPlay.FlatAppearance.MouseDownBackColor = Color.Transparent;

            //Thiet lap nut btnCreate
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.BackColor = Color.Transparent;
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnCreate.FlatAppearance.MouseDownBackColor = Color.Transparent;
        }

        void lobby_FormClosed(object sender, EventArgs e)
        {
            Client_Socket.datatype = "DISCONNECT";
            Client_Socket.SendMessage(Player.name);
            Client_Socket.clientSocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
            Client_Socket.clientSocket.Close();
            this.Show();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(username.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên người chơi!");
                    return;
                }
                    //Khoi tao form sanh cho
                    lobby = new GiaoDienTaoPhong();

                    //Thiet lap ket noi
                    IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);
                    Client_Socket.Connect(serverEP);

                    //Gui thong diep ket noi
                    Client_Socket.datatype = "CONNECT";
                    Player.name = username.Text;
                    Client_Socket.SendMessage(username.Text);

                    this.Hide();
                    lobby.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(username.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên người chơi!");
                    return;
                }
                    //Khoi tao form sanh cho
                    lobby = new GiaoDienTaoPhong();

                    //Thiet lap ket noi
                    IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);
                    Client_Socket.Connect(serverEP);

                    //Gui thong diep ket noi
                    Client_Socket.datatype = "CONNECT";
                    Player.name = username.Text;
                    Client_Socket.SendMessage(username.Text);

                    this.Hide();
                    lobby.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

    }
}