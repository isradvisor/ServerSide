using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class TripPoint
    {
        public string TouristEmail { get; set; }
        public string GuideEmail { get; set; }
        public string AttractionName { get; set; }
        public string Address { get; set; }
        public string AreaName { get; set; }
        public string Region { get; set; }
        public string FullDescription { get; set; }
        public string Opening_Hours { get; set; }
        public object FromHour { get; set; }
        public object ToHour { get; set; }

        public List<TripPoint> AddTripToTourist(List<TripPoint> tripPoints)
        {
            DBservices db = new DBservices();
            db.DeleteLastTripTourist(tripPoints[0].TouristEmail);
            for (int i = 0; i < tripPoints.Count; i++)
            {
                db.AddPointToSQL(tripPoints[i]);
            }
            
            return db.GetAllPointsOfTourist(tripPoints[0].TouristEmail);
        }

        public List<TripPoint> GetAllPointsFromSQL(string email)
        {
            DBservices db = new DBservices();
           return db.GetAllPointsOfTourist(email);
        }
    }
}