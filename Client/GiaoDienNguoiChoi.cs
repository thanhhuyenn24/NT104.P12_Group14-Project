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
        //UNDO
        private Stack<Bitmap> undoStack;    // Thêm stack để lưu lịch sử các thao tác
        private const int MAX_UNDO_LEVELS = 20; // Giới hạn số lượng undo để tránh tốn bộ nhớ
        //
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
           
            //Cài đặt cho phần vẽ
            bm = new Bitmap(pic.Width, pic.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            pic.Image = bm;

            //UNDO
            // Khởi tạo undoStack
            undoStack = new Stack<Bitmap>();
            // Lưu trạng thái ban đầu
            SaveState();
            try
            {
                undoStack = new Stack<Bitmap>();
                ResetCanvas(); // Sử dụng hàm ResetCanvas thay vì khởi tạo trực tiếp
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo: " + ex.Message);
            }
        }

        //Khai báo phần vẽ 
        Bitmap bm;
        Graphics g;
        bool paint = false;
        Point px, py;
        Pen p = new Pen(Color.Black, 1);
        Pen erase=new Pen(Color.White, 20);
        int index;

        ColorDialog cd=new ColorDialog();
        Color new_color;


        //CAC HAM CHO UNDO________________________________________________________________________________________
        // Hàm sao chép bitmap
        private Bitmap CloneBitmap(Bitmap sourceBitmap)
        {
            if (sourceBitmap == null)
                return null;

            try
            {
                Rectangle rect = new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height);
                Bitmap newBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height,
                                            System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                using (Graphics g = Graphics.FromImage(newBitmap))
                {
                    g.DrawImage(sourceBitmap, rect, rect, GraphicsUnit.Pixel);
                }

                return newBitmap;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Hàm lưu trạng thái
        private void SaveState()
        {
            try
            {
                if (bm == null) return;

                // Tạo bản sao của bitmap hiện tại
                Bitmap clonedBitmap = CloneBitmap(bm);

                if (clonedBitmap != null)
                {
                    if (undoStack.Count >= MAX_UNDO_LEVELS)
                    {
                        // Xóa trạng thái cũ nhất một cách an toàn
                        try
                        {
                            var oldestState = undoStack.Pop();
                            if (oldestState != null)
                            {
                                oldestState.Dispose();
                            }
                        }
                        catch { }
                    }

                    undoStack.Push(clonedBitmap);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lưu trạng thái: " + ex.Message);
            }
        }

        // Thêm hàm để reset canvas khi có lỗi
        private void ResetCanvas()
        {
            try
            {
                bm = new Bitmap(pic.Width, pic.Height);
                g = Graphics.FromImage(bm);
                g.Clear(Color.White);
                pic.Image = bm;
                SaveState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể khôi phục canvas: " + ex.Message);
            }
        }

        private void btn_undo_Click(object sender, EventArgs e)
        {
            try
            {
                if (undoStack.Count >= 0)
                {

                    // Lấy trạng thái trước đó
                    bm = undoStack.Pop();

                    if (bm != null)
                    {
                        g = Graphics.FromImage(bm);
                        pic.Image = bm;
                        pic.Refresh();
                    }
                    else
                    {
                        // Tạo bitmap mới nếu có lỗi
                        ResetCanvas();
                    }
                }
            }
            catch (Exception ex)
            {
                ResetCanvas();
            }
        }
        //_______________________________________________________________________________________________________


        //HÀM + EVENT ĐỂ VẼ______________________________________________________________________________________
        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            py = e.Location;
        }
        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if(paint)
            {
                if(index==2)
                {
                    px = e.Location;
                    g.DrawLine(erase,px,py);
                    py=px;
                }
            }
            if (paint)
            {
                if (index == 1)
                {
                    px = e.Location;
                    g.DrawLine(p, px, py);
                    py = px;
                }
            }
            pic.Refresh();
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;
            SaveState(); // Lưu trạng thái sau khi vẽ xong
        }

        private void btn_pen_Click(object sender, EventArgs e)
        {
            index = 1;
        }

        private void btn_eraser_Click(object sender, EventArgs e)
        {
            index = 2;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            SaveState(); // Lưu trạng thái trước khi xóa
            g.Clear(Color.White);
            pic.Image = bm;
            index = 0;
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            new_color = cd.Color;
            pic_color.BackColor = cd.Color;
            p.Color = cd.Color;
        }
        //_______________________________________________________________________________________________________

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
