using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Player
    {
        public string name { get; set; }
        public int score { get; set; } = 0;
        //public int totalscore { get; set; }
        public int turn { get; set; }
        public Socket playerSocket { get; set; }
    }
}
