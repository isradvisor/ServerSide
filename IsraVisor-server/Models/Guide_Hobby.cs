using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class Guide_Hobby
    {
        public int guidegCode { get; set; }
        public int HobbyHCode { get; set; }

        //מקבלת רשימת התמחויות של מדריך ספציפי
        public List<Hobby> GetGuideHobbiesFromSQLBygCode(int id)
        {
            DBservices db = new DBservices();
            return db.GetGuideHobbies(id);
        }

        //מכניסה רשימת התמחויות של מדריך ספציפי
        public List<Hobby> PostGuideListHobbies(List<Guide_Hobby> guideHobbiesList)
        {
            DBservices db = new DBservices();
            db.DeleteAllGuideHobbies(guideHobbiesList[0].guidegCode);
            for (int i = 0; i < guideHobbiesList.Count; i++)
            {
                db.PostGuideHobbiesToSQL(guideHobbiesList[i]);
            }
            return db.GetGuideHobbies(guideHobbiesList[0].guidegCode);
        }

        //מוחקת התמחויות של מדריך ספציפי
        public void DeleteGuideHobbies(int id)
        {
            DBservices db = new DBservices();
            db.DeleteAllGuideHobbies(id);
        }
    }
}