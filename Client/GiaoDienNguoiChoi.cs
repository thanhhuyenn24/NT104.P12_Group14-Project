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

        public GiaoDienNguoiChoi(string playerName)
        {
            InitializeComponent();

            // Hiển thị tên người chơi trên label hoặc các thành phần giao diện khác
            label5.Text = playerName;
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
