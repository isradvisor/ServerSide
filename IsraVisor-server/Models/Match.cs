using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class Match
    {
        public int Language { get; set; }
        public int Age { get; set; }
        public List<int> Hobbies { get; set; }
        public List<int> Expertises { get; set; }
        public double Rank { get; set; }
        public int Id1 { get; set; }
        public int Id2 { get; set; }
    }
}