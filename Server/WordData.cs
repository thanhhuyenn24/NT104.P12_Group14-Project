using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class WordData
    {
        public static List<string> wordList = new List<string>()
    {
        "apple", "car", "tree", "house", "dog", "cat", "sun", "ball", "book", "plane"
        // Thêm các từ khác
    };

        public static List<string> GetRandomWords(int count)
        {
            Random random = new Random();
            return wordList.OrderBy(x => random.Next()).Take(count).ToList(); // Trả về một danh sách từ ngẫu nhiên
        }
    }
}
