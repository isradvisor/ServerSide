using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class AreaPointInTrip
    {
        public int TripPlan_IdPlan { get; set; }
        public int PointInPlanId { get; set; }
        public object FromDate { get; set; }
        public string AreaName { get; set; }
        public int OrderNumber { get; set; }
        public List<AttractionPointInTrip> Attractions { get; set; }

        public AreaPointInTrip AddTripToTourist(AreaPointInTrip trip)
        {
            DBservices db = new DBservices();
            if (db.AddTripTouristToSQL(trip) == 1)
            {
                return trip;

            }
            else
            {
                return null;
            }
        }

        public List<AreaPointInTrip> GetCitiesByIdPlan(int id)
        {
            DBservices db = new DBservices();
            return db.GetCitiesFromSQL(id);
        }
    }
}