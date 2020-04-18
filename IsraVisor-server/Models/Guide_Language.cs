using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class Guide_Language
    {
        public int Guide_Code { get; set; }
        public int Language_Code { get; set; }

        //מקבלת רשימת שפות מהSQL
        public List<Guide_Language> ReadGuideLangsFromSQL()
        {
            DBservices db = new DBservices();
            return db.GetGuideLangsFromSQL();
        }

        //מכניסה שפות של מדריך ספציפי//לבדוק אם הכרחי הפונקציה
        public int PostLanguagesGuideToSQL(Guide_Language guidesLanguages)
        {
            DBservices db = new DBservices();
            int numAffected = db.PostGuideLanguagesToSQL(guidesLanguages);
            return numAffected;
        }

        //מקבלת את כל השפות של מדריך ספציפי
        public List<Guide_Language> ReadAllGuideLanguages(int id)
        {
            DBservices db = new DBservices();
            return db.ReadGuideAllLanguagesFromSQL(id);
        }

        //מכניסה שפות של מדריך ספציפי
        public List<Guide_Language> PostGuideLanguagesToSQL(List<Guide_Language> guideLan)
        {
            DBservices db = new DBservices();
            db.DeleteAllGuideLanguages(guideLan[0].Guide_Code);
            for (int i = 0; i < guideLan.Count; i++)
            {
                db.PostGuideLanguagesToSQL(guideLan[i]);
            }
            return db.ReadGuideAllLanguagesFromSQL(guideLan[0].Guide_Code);
        }

        //מוחקת את השפות של המדריך הספציפי
        public void DeleteGuideLanguages(int id)
        {
            DBservices db = new DBservices();
            db.DeleteAllGuideLanguages(id);
        }
    }
}