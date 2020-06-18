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
        public string FullDescription { get; set; }
        public string Address { get; set; }
        public string Attraction_Type { get; set; }
        public string Region { get; set; }

        public int postAllAtractionsToSQL(List<Attraction> attractions)
        {
            DBservices db = new DBservices();
            int num = 0;
            for (int i = 0; i < attractions.Count; i++)
            {
                num += db.postAllAtt(attractions[i]);
            }
            return num;
        }

        public List<Attraction> GetAllAttractionsFromSQL()
        {
            DBservices db = new DBservices();
           return db.GetAllAttractionsFromSQL();
        }
    }
}