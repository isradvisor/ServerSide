using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class Attraction
    {
        public int AttractionCode { get; set; }
        public string AttractionName { get; set; }
        public string AreaName { get; set; }
        public string Opening_Hours { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}