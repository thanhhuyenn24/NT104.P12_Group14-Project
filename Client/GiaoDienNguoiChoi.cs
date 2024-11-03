using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Server;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using Timer = System.Windows.Forms.Timer;


namespace Client
{
    public partial class GiaoDienNguoiChoi : Form
    {
        public string round { get; set; }
        public string word { get; set; }
        private int score = 0;
        //UNDO
        private Stack<Bitmap> undoStack;    //Stack de luu lich ssu cac thao tac
        private const int MAX_UNDO_LEVELS = 20; //Gioi han so luong undo tranh ton bo nho
        
        public GiaoDienNguoiChoi()
        {
            InitializeComponent();

            btn_undo.FlatStyle = FlatStyle.Flat;
            btn_undo.BackColor = Color.Transparent;
            btn_undo.FlatAppearance.BorderSize = 0;
            btn_undo.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn_undo.FlatAppearance.MouseDownBackColor = Color.Transparent;

            btn_clear.FlatStyle = FlatStyle.Flat;
            btn_clear.BackColor = Color.Transparent;
            btn_clear.FlatAppearance.BorderSize = 0;
            btn_clear.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn_clear.FlatAppearance.MouseDownBackColor = Color.Transparent;

            btn_send.FlatStyle = FlatStyle.Flat;
            btn_send.BackColor = Color.Transparent;
            btn_send.FlatAppearance.BorderSize = 0;
            btn_send.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn_send.FlatAppearance.MouseDownBackColor = Color.Transparent;


            //Cai dat phan ve
            bm = new Bitmap(pic.Width, pic.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            pic.Image = bm;

            //UNDO
            //Khoi tao undoStack
            undoStack = new Stack<Bitmap>();
            //Luu trang thai ban dau
            SaveState();
            try
            {
                undoStack = new Stack<Bitmap>();
                ResetCanvas(); //Su dung ham ResetCanvas thay vi khoi tao truc tiep
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo: " + ex.Message);
            }
        }
        #region DRAW
        //Khai bao phan ve
        Bitmap bm;
        Graphics g;
        bool paint = false;
        Point px, py;
        Pen p = new Pen(Color.Black, 1);
        Pen erase = new Pen(Color.White, 20);
        int index;

        ColorDialog cd = new ColorDialog();
        Color new_color;

        //HAM + EVENT DE VE
        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            py = e.Location;
        }
        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (paint)
            {
                if (index == 2)
                {
                    px = e.Location;
                    g.DrawLine(erase, px, py);
                    py = px;
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
            SaveState(); //Luu trang thai sau khi ve xong

            Client_Socket.datatype = "PIC_CHANGE";


            //Chuyen doi bitmap thanh string
            string stringImage = BitmapToString(bm);

            //Gui string cho server
            Client_Socket.SendMessage(stringImage);
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
            SaveState(); //Luu trang thai truoc khi xoa
            Clear_pic();
            Client_Socket.datatype = "PIC_CHANGE";

            //Chuyen bitmap thanh string
            string stringImage = BitmapToString(bm);

            //Gui string cho server
            Client_Socket.SendMessage(stringImage);
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            new_color = cd.Color;
            pic_color.BackColor = cd.Color;
            p.Color = cd.Color;
        }
        #endregion

        #region UNDO
        //Ham sao chep bitmap
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

        //Ham luu trang thai
        private void SaveState()
        {
            try
            {
                if (bm == null) return;

                //Tao ban sao cua bitmap hien tai
                Bitmap clonedBitmap = CloneBitmap(bm);

                if (clonedBitmap != null)
                {
                    if (undoStack.Count >= MAX_UNDO_LEVELS)
                    {
                        //Xoa trang thai cu nhat
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

        //Ham de reset canvas khi co loi
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

                    //Lay trang thai truoc do
                    bm = undoStack.Pop();

                    if (bm != null)
                    {
                        g = Graphics.FromImage(bm);
                        pic.Image = bm;
                        pic.Refresh();
                    }
                    else
                    {
                        //Tao bitmap moi khi co loi
                        ResetCanvas();
                    }
                }
                Client_Socket.datatype = "PIC_CHANGE";


                //Chuyen bitmap thanh string
                string stringImage = BitmapToString(bm);

                //Gui string cho server
                Client_Socket.SendMessage(stringImage);
            }
            catch (Exception ex)
            {
                ResetCanvas();
            }
        }
        #endregion

        #region DISPLAY
        
        //Hien thi danh sach ten + diem tung nguoi choi
        public void InGameDisplay()
        {

            lb1.Text = Player.name;
            tb1.Text = Player.score.ToString();
            tb1.Tag = Player.name;

            // Sắp xếp danh sách người chơi theo thứ tự turn
            Client_Socket.otherPlayers.Sort((x, y) => x.turn.CompareTo(y.turn));

            int playerCount = Client_Socket.otherPlayers.Count;
            //MessageBox.Show(playerCount.ToString());

            for (int i = 0; i < playerCount; i++)
            {
                var player = Client_Socket.otherPlayers[i];

                if (i == 0)
                {
                    panel2.Visible = true;
                    lb2.Text = player.name;
                    tb2.Text = player.score;
                    tb2.Tag = player.name;
                }
                else if (i == 1)
                {
                    panel3.Visible = true;
                    lb3.Text = player.name;
                    tb3.Text = player.score;
                    tb3.Tag = player.name;
                }
                else if (i == 2)
                {
                    panel4.Visible = true;
                    lb4.Text = player.name;
                    tb4.Text = player.score;
                    tb4.Tag = player.name;
                }
                else if (i == 3)
                {
                    panel5.Visible = true;
                    lb5.Text = player.name;
                    tb5.Text = player.score;
                    tb5.Tag = player.name;
                }
            }
        }

        //Thong bao ai dang ve
        public void Turn_Notify(string Name)
        {
            Thread.Sleep(1500);
            if (Player.name == Name)
            {
                tbCmt.Text = "Your turn \r\n";
            }
            else
                tbCmt.Text = Name + "'s turn";
        }

        //Nguoi ve duoc ve, khong duoc doan, hien thi tu can ve
        public void Allow_Playing()
        {
            pic.Enabled = true;
            btn_clear.Enabled = true;
            btn_color.Enabled = true;
            btn_eraser.Enabled = true;
            btn_pen.Enabled = true;
            btn_undo.Enabled = true;
            btn_send.Enabled = false;
            pnlWORD.Visible = true;
            WORD.Text = word;
        }

        //Nguoi doan khong duoc ve, duoc doan
        public void NotAllowPlaying()
        {
            pic.Enabled=false;
            btn_clear.Enabled=false;
            btn_color.Enabled=false;
            btn_eraser.Enabled=false;
            btn_pen.Enabled=false;
            btn_undo.Enabled=false;
            btn_send.Enabled = true;
            pnlWORD.Visible=false;
        }

        //Doan dung hien thi Ten nguoi choi + guessed right!
        public void Game_Update_RIGHT(string name)
        {
            status.Text += name + " guessed right!\n";

        }

        //Doan sai hien thi ten nguoi choi + tu nguoi choi da doan
        public void Game_Update_WRONG(string name, string w)
        {
            status.Text += name + ": " + w + "\n";

        }
        //Cap nhat diem cua nguoi choi khac khi co thay doi
        public void Score_Update(string Name, string Score)
        {
            foreach (Control control in Controls)
            {
                //Neu control la panel, tim nhung control con cua no
                if (control is Panel panel)
                {
                    foreach (Control panelControl in panel.Controls)
                    {
                        if (panelControl is TextBox textBox &&
                            textBox.Tag != null &&
                            textBox.Tag.ToString() == Name)
                        {
                            textBox.Text = Score;
                            return;
                        }
                    }
                }
            }
        }

        //Xoa hinh ve khi bat dau luot moi
        public void Clear_pic()
        {
            g.Clear(Color.White);
            pic.Image = bm;
            index = 0;
        }

        //Doan dung se khong doan tiep cho toi khi bat dau luot moi
        public void GR()
        {
            btn_send.Enabled = false;
        }
        #endregion

        //Gui thong diep doan dung/sai
        private void btn_send_Click(object sender, EventArgs e)
        {
            if (tbx_send.Text == word)
            {
                Client_Socket.datatype = "GUESS_RIGHT";
                Client_Socket.SendMessage(Player.name);
            }
            else
            {
                Client_Socket.datatype = "GUESS_WRONG";
                string msg = Player.name + ";" + tbx_send.Text;
                Client_Socket.SendMessage(msg);
            }
        }

        // Chuyen Bitmap thanh string gui cho server
        public static string BitmapToString(Bitmap bitmap)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    //Su dung dinh dang PNG dam bao chat luong
                    bitmap.Save(ms, ImageFormat.Png);
                    byte[] bytes = ms.ToArray();
                    return Convert.ToBase64String(bytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in BitmapToString: {ex.Message}");
                return null;
            }
        }
    }
}
