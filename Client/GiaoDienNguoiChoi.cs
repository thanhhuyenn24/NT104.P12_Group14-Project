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
        int drawTime;

        public GiaoDienNguoiChoi(string playerName, int drawTime)
        {
            InitializeComponent();

            // Hiển thị tên người chơi trên label hoặc các thành phần giao diện khác
            label5.Text = playerName;

            this.drawTime = drawTime;
            labelTimer.Text = drawTime.ToString();
            StartCountdown(); // Bắt đầu đếm ngược khi form mở
        }

        private Timer timer;

        private void StartCountdown()
        {
            timer = new Timer();
            timer.Interval = 1000; // Mỗi giây
            timer.Tick += Timer_Tick; // Gắn sự kiện Tick
            timer.Start(); // Bắt đầu đếm ngược
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (drawTime > 0)
            {
                drawTime--; // Giảm thời gian
                labelTimer.Text = drawTime.ToString(); // Cập nhật trên Label
            }
            else
            {
                timer.Stop(); // Dừng Timer khi đếm ngược hoàn thành
                MessageBox.Show("No time!"); // Thông báo hết giờ
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
