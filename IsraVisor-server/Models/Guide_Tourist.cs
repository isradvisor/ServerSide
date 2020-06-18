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
        public string Status { get; set; }
        public string Token { get; set; }

        public IEnumerable<Guide_Tourist> GetAllRequests()
        {
            DBservices db = new DBservices();
            return db.GetAllRequests();
        }

        public string TouristEmail { get; set; }
        public string GuideEmail { get; set; }
        //מקבלת את רשימת הניקודים שתייר ספציפי נתן למדריכים
        public Guide_Tourist GetRankByID(int id)
        {
            DBservices db = new DBservices();
            return db.GetGuidesRankByID(id);
        }

        public Guide_Tourist GetTouristStatus(string email)
        {
            DBservices db = new DBservices();
            return db.GetTouristStatus(email);
        }

        public List<Guide_Tourist> GetRequestsFromSQL(string email)
        {
            DBservices db = new DBservices();
            return db.GetAllGuideRequests(email);
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

        public List<Tourist> GetAllTouristsOfGuide(string email)
        {
            DBservices db = new DBservices();
            List<string> emails = db.GetAllTouristsOfGuide(email);
            List<Tourist> tourists = new List<Tourist>();
            for (int i = 0; i < emails.Count; i++)
            {
                tourists.Add(db.GetAllDetailsFromSQL(emails[i]));
            }
            return tourists;
        }

        //מכניסה ניקוד חדש שתייר נתן למדריך
        public void PostGuideTouristRank(Guide_Tourist guide_Tourist)
        {
            DBservices db = new DBservices();
            db.PostRankGuideByTourist(guide_Tourist);
            GetAllRanksOfGuide(guide_Tourist.guidegCode);
        }

        public List<Guide_Tourist> UpdateRequest(Guide_Tourist g)
        {
            DBservices db = new DBservices();
            if (db.UpdateTouristRequestInSQL(g) == 1)
            {
                return db.GetAllGuideRequests(g.GuideEmail);
            }
            else
            {
                return null;
            }
        }

        public int AddRequest(Guide_Tourist gt)
        {
            DBservices db = new DBservices();
           int num = db.AddRequest(gt);
            return num;
         
        }

        public List<Guide_Tourist> GetAllUsersInChatToken()
        {
            DBservices db = new DBservices();
            return db.GetTokensOfUsersInChat();
        }
    }
}