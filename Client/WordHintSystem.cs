using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Client
{
    public class WordHintSystem
    {
        private string actualWord;
        private char[] displayedWord;
        private List<int> remainingPositions;
        private Random random;
        private Label wordLabel;
        private int totalTime;

        public WordHintSystem(string word, Label label, int initialTime)
        {
            actualWord = word;
            wordLabel = label;
            random = new Random();
            totalTime = initialTime;
            InitializeWord();
        }

        private void InitializeWord()
        {
            //Tao mang displayedWord voi do dai bang tu goc
            displayedWord = new char[actualWord.Length];
            remainingPositions = new List<int>();

            //Duyet tung ki tu trong tu goc
            for (int i = 0; i < actualWord.Length; i++)
            {
                if (actualWord[i] == ' ')
                {
                    //Neu la khoang cach, hien thi
                    displayedWord[i] = ' ';
                }
                else
                {
                    //Neu la chu cai, thay bang gach duoi va them vao danh sach can hien
                    displayedWord[i] = '_';
                    remainingPositions.Add(i);
                }
            }

            UpdateDisplay(totalTime);
        }

        public void UpdateTime(int currentTime)
        {
            //Tinh nguong thoi gian bat dau hien hint
            int timeThreshold = totalTime / 2;
            //Neu thoi gian <= nguong va con chu cai chua hien
            if (currentTime <= timeThreshold && remainingPositions.Count > 0)
            {
                //Tinh so chu cai can hien trong thoi diem nay
                // Cong thuc: (thoi gian nguong - thoi gian hien tai) / (thoi gian nguong / so chu cai con lai)
                int lettersToReveal = (int)((timeThreshold - currentTime) / (timeThreshold / (float)actualWord.Length));

                //Hien so chu cai da tinh
                while (remainingPositions.Count > actualWord.Length - lettersToReveal)
                {
                    RevealRandomLetter();
                }
            }

            UpdateDisplay(currentTime);
        }

        private void RevealRandomLetter()
        {
            if (remainingPositions.Count > 0)
            {
                //Chon ngau nhien 1 vi tri tu danh sach con lai
                int index = random.Next(remainingPositions.Count);
                int positionToReveal = remainingPositions[index];

                //Hien chu cai tai vi tri do
                displayedWord[positionToReveal] = actualWord[positionToReveal];

                //Xoa vi tri da hien khoi danh sach
                remainingPositions.RemoveAt(index);
            }
        }

        private void UpdateDisplay(int timeLeft)
        {
            //Su dung Invoke de cap nhat UI tu thread khac neu can
            if (wordLabel.InvokeRequired)
            {
                wordLabel.Invoke(new Action(() => UpdateDisplay(timeLeft)));
                return;
            }

            string displayText = string.Join(" ", displayedWord);
            wordLabel.Text = displayText;

        }
    }
}
