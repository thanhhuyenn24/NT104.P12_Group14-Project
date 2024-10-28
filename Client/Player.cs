using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Player
    {
        public static string name { get; set; }
        public static int turn { get; set; }
        public static int score { get; set; } //Diem trong 1 vong
        public static int totalScore { get; set; } //Diem trong ca 3 vong
        public Player() { }
        public Player(string Name, int Turn, int Score)
        {
            name = Name;
            turn = Turn;
            score = Score;
        }
    }

    public class OtherPlayers
    {
        public string name { get; set; }
        public string turn { get; set; }
        public string score { get; set; }
        public string totalScore { get; set; }
    }
}
