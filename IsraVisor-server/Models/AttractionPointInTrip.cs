using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class AttractionPointInTrip
    {
        public int TripPlan_IdPlan { get; set; }
        public int PointInPlanId { get; set; }
        public int PointNo { get; set; }
        public object fromHour { get; set; }
        public object ToHour { get; set; }
        public int AttractionCode { get; set; }
        public string AttractionName { get; set; }
        public string CityName { get; set; }
        public string Opening_Hours { get; set; }

        public AttractionPointInTrip AddAttractionToSQL(AttractionPointInTrip attraction)
        {
            DBservices db = new DBservices();
            if (db.AddAtraction(attraction) == 1)
            {
                return attraction;
            }
            else
            {
                return null;
            }
        }

        public List<AttractionPointInTrip> GetAttractionsById(int id)
        {
            DBservices db = new DBservices();
            return db.GetAttractionsFromSQLByID(id);
        }
    }
}