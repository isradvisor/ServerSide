using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class Guide_Tourist
    {
        public int guidegCode { get; set; }
        public int TouristId { get; set; }
        public int Rank { get; set; }
        public object DateOfRanking { get; set; }
        public string Comment { get; set; }


        //מקבלת את רשימת הניקודים שתייר ספציפי נתן למדריכים
        public Guide_Tourist GetRankByID(int id)
        {
            DBservices db = new DBservices();
            return db.GetGuidesRankByID(id);
        }

        //מקבלת רשימת ניקודים של מדריך ספציפי שקיבל מתיירים
        public double GetAllRanksOfGuide(int id)
        {
            DBservices db = new DBservices();
            double sum = 0;
            List<Guide_Tourist> listRanks = db.GetAllRanksOfGuide(id);
            for (int i = 0; i < listRanks.Count; i++)
            {
                sum += listRanks[i].Rank;
            }
            sum = sum / listRanks.Count; //מחשבת ניקוד ממוצע
             db.UpdateRankGuide(id, sum); //מעדכנת ניקוד של המדריך
            return sum;
        }

        //מכניסה ניקוד חדש שתייר נתן למדריך
        public void PostGuideTouristRank(Guide_Tourist guide_Tourist)
        {
            DBservices db = new DBservices();
            db.PostRankGuideByTourist(guide_Tourist);
            GetAllRanksOfGuide(guide_Tourist.guidegCode);
        }
    }
}