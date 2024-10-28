using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Client
{
    public partial class WordSelectionForm : Form
    {
        public string SelectedWord { get; private set; }
        public event Action<string> WordSelected; // Tạo event khi từ được chọn

        public WordSelectionForm(List<string> words)
        {
            InitializeComponent();
            foreach (string word in words)
            {
                listBoxWords.Items.Add(word);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (listBoxWords.SelectedItem != null)
            {
                SelectedWord = listBoxWords.SelectedItem.ToString();
                WordSelected?.Invoke(SelectedWord); // Gọi event khi chọn xong từ
                DialogResult = DialogResult.OK; // Đóng Form
            }
            else
            {
                MessageBox.Show("Please choose.");
            }
        }
    }
}
