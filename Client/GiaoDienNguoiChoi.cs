using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Server; // Thay 'ServerNamespace' bằng namespace thực tế của WordData


namespace Client
{
    public partial class GiaoDienNguoiChoi : Form
    {
        private List<string> players;
        private int drawTime;
        private string playerName;
        private string role;
        private static Timer sharedTimer; // Sử dụng Timer chia sẻ để đồng bộ hóa
        private GiaoDienTaoPhong taoPhongForm; // Tham chiếu đến GiaoDienTaoPhong

        public GiaoDienNguoiChoi(string playerName, string role, int drawTime, GiaoDienTaoPhong taoPhongForm)
        {
            InitializeComponent();
            label5.Text = playerName;
            this.playerName = playerName;
            this.role = role;
            this.drawTime = drawTime;

            if (role == "Drawer")
            {
                DisplayWordSelection(); // Người Vẽ hiển thị hộp chọn từ
            }
            else
            {
                StartCountdown(); // Người Đoán chỉ bắt đầu đếm ngược
            }

            this.taoPhongForm = taoPhongForm;
        }

        private void DisplayWordSelection()
        {
            int wordCount = Convert.ToInt32(GiaoDienTaoPhong.Instance.comboBoxWordCount.SelectedItem);
            List<string> randomWords = WordData.GetRandomWords(wordCount); // Lấy từ ngẫu nhiên từ Server
            string selectedWord = ShowWordSelectionDialog(randomWords);

            if (!string.IsNullOrEmpty(selectedWord))
            {
                StartCountdown(); // Bắt đầu đếm ngược sau khi chọn từ
                BroadcastWord(selectedWord); // Truyền từ cho người Đoán
            }
        }

        private void BroadcastWord(string selectedWord)
        {
            foreach (var form in Application.OpenForms.OfType<GiaoDienNguoiChoi>())
            {
                if (form.role == "Guesser")
                {
                    form.ReceiveWord(selectedWord);
                }
            }
        }

        public void ReceiveWord(string word)
        {
            // Cập nhật giao diện hoặc hiển thị gợi ý nếu cần
        }

        private string ShowWordSelectionDialog(List<string> words)
        {
            string wordSelection = string.Join("\n", words);
            DialogResult result = MessageBox.Show(wordSelection + "\n\nChoose and Begin",
                                                  "Choose word",
                                                  MessageBoxButtons.OK);

            return result == DialogResult.OK ? words[0] : null; // Cập nhật phần chọn từ
        }

        private Timer timer;

        private void StartCountdown()
        {
            if (sharedTimer == null)
            {
                sharedTimer = new Timer();
                sharedTimer.Interval = 1000;
                sharedTimer.Tick += Timer_Tick;
            }

            sharedTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (drawTime > 0)
            {
                drawTime--;
                labelTimer.Text = drawTime.ToString();
            }
            else
            {
                sharedTimer.Stop();
                MessageBox.Show("Hết giờ!");
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
