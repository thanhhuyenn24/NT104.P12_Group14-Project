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

    public partial class GiaoDienTaoPhong : Form
    {
        public static GiaoDienTaoPhong Instance { get; private set; }

        List<string> players = new List<string>();

        public GiaoDienTaoPhong()
        {
            InitializeComponent();
            Instance = this; // Gán Instance trong constructor
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lấy số lượng người chơi từ combobox
            int playerCount = Convert.ToInt32(comboBoxPlayers.SelectedItem);
            int drawTime = Convert.ToInt32(comboBoxDrawTime.SelectedItem);

            // Nhập tên cho mỗi người chơi
            for (int i = 1; i <= playerCount; i++)
            {
                string playerName = Microsoft.VisualBasic.Interaction.InputBox($"Enter name for Player {i}:", "Player Name", $"Player {i}");
                if (!string.IsNullOrWhiteSpace(playerName))
                {
                    players.Add(playerName);
                }
            }

            // Chọn ngẫu nhiên một người làm Người Vẽ
            Random random = new Random();
            int drawerIndex = random.Next(0, players.Count);
            string drawer = players[drawerIndex];

            // Khởi tạo các Form GiaoDienNguoiChoi cho từng người chơi
            for (int i = 0; i < players.Count; i++)
            {
                string role = (i == drawerIndex) ? "Drawer" : "Guesser"; // Phân công vai trò
                GiaoDienNguoiChoi gameForm = new GiaoDienNguoiChoi(players[i], role); // Truyền tên và vai trò vào Form
                gameForm.Show();
            }
        }

        private void comboBoxPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxWordCount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
