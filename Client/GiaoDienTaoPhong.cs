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
            int numberOfPlayers = Convert.ToInt32(comboBoxPlayers.SelectedItem); // Lấy số người chơi

            for (int i = 1; i <= numberOfPlayers; i++)
            {
                // Hiển thị hộp thoại để người chơi nhập tên
                string playerName = Microsoft.VisualBasic.Interaction.InputBox($"Enter name for Player {i}:", "Player Name", "Player" + i);

                if (!string.IsNullOrWhiteSpace(playerName))
                {
                    // Lưu tên người chơi và chuyển tới form tiếp theo
                    // Giả sử chúng ta lưu tên người chơi vào danh sách
                    players.Add(playerName);
                }
            }

            // Chuyển tới GiaoDienNguoiChoi sau khi nhận tên
            GiaoDienNguoiChoi gameForm = new GiaoDienNguoiChoi(players); // Truyền danh sách tên người chơi sang form khác
            gameForm.Show();
            this.Hide();
        }

        private void comboBoxPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
