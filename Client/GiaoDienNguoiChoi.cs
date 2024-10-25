using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class GiaoDienNguoiChoi : Form
    {
        private List<string> players;

        public GiaoDienNguoiChoi(List<string> playersList)
        {
            InitializeComponent();
            players = playersList;

            // Hiển thị tên người chơi trong các Label hoặc ListBox
            DisplayPlayers();
        }

        private void DisplayPlayers()
        {
            for (int i = 0; i < players.Count; i++)
            {
                // Ví dụ: Hiển thị tên của người chơi đầu tiên trên Label1
                if (i == 0)
                {
                    label5.Text = players[i];
                }
                // Tương tự cho các Label khác
            }
        }
        public GiaoDienNguoiChoi()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Chart_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
