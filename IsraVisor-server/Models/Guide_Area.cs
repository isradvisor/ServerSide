using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class Guide_Area
    {
        public int Guide_Code { get; set; }
        public int Area_Code { get; set; }

        public List<Guide_Area> ReadAllAreasByGuide(int id)
        {
            DBservices db = new DBservices();
           return db.GetAreasByGuideFromSQL(id);
        }

        public List<Guide_Area> PostGuideAreasToSQL(List<Guide_Area> guide_AreasList)
        {
            DBservices db = new DBservices();
            db.DeleteAllGuideAreas(guide_AreasList[0].Guide_Code);
            for (int i = 0; i < guide_AreasList.Count; i++)
            {
                db.PostGuideAreasToSQL(guide_AreasList[i]);
            }
            return ReadAllAreasByGuide(guide_AreasList[0].Guide_Code);
        }
    }
}