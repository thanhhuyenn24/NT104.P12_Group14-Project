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
    public partial class GiaoDienChinh : Form
    {
        public GiaoDienChinh()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            GiaoDienTaoPhong taoPhongForm = new GiaoDienTaoPhong();

            // Hiển thị form mới
            taoPhongForm.Show();  // Sử dụng Show() để mở form không khóa form hiện tại

            // Nếu bạn muốn form mới là modal (chặn tương tác với form hiện tại cho đến khi đóng)
            // bạn có thể sử dụng ShowDialog() thay cho Show().
        }
    }
}
