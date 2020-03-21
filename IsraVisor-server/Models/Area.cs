using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class Area
    {
        public string AreaName { get; set; }

        static public List<Area> AreaList = new List<Area>();

        public void PostAllCitiesToSQL(List<Area> allCities)
        {
            DBservices db = new DBservices();

            for (int i = 0; i < allCities.Count; i++)
            {
                db.PostCitiesToSQL(allCities[i]);

            }

        }

        public List<Area> GetAllCitiesFromSQL()
        {
            DBservices db = new DBservices();
           return db.GetAreasFromSQL();
        }

       
    }
}