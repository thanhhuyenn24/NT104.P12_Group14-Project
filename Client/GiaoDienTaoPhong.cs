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
        List<string> players = new List<string>();

        public GiaoDienTaoPhong()
        {
            InitializeComponent();
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

            // Vòng lặp để nhập tên từng người chơi và hiển thị form tương ứng
            for (int i = 1; i <= playerCount; i++)
            {
                // Hiển thị InputBox để nhập tên người chơi
                string playerName = Microsoft.VisualBasic.Interaction.InputBox($"Enter name for Player {i}:", "Player Name", $"Player {i}");

                // Kiểm tra xem tên có hợp lệ không
                if (!string.IsNullOrWhiteSpace(playerName))
                {
                    // Thêm tên người chơi vào danh sách
                    players.Add(playerName);

                    // Tạo form GiaoDienNguoiChoi mới và truyền tên người chơi
                    GiaoDienNguoiChoi gameForm = new GiaoDienNguoiChoi(playerName);

                    // Hiển thị form (không khóa form hiện tại)
                    gameForm.Show();
                }
                else
                {
                    MessageBox.Show("Player name cannot be empty. Please enter a valid name.");
                }
            }

        }

        private void comboBoxPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
