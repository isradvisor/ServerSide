using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class Match
    {
        public List<int> Language { get; set; }
        public int Age { get; set; }
        public List<int> Hobbies { get; set; }
        public List<int> Expertises { get; set; }
        public double Rank { get; set; }
        public int Id { get; set; }

        //מקבלת רשימה של כל המדריכים ע"פ פורמט ההשוואה של מחלקת הנירמול
        public List<Match> GetGuidesDetails()
        {
            DBservices db = new DBservices();
            List<Guide> listGuides = db.GetGuidesDetailsMatch();
            List<Match> listMatch = new List<Match>();
            for (int i = 0; i < listGuides.Count; i++)
            {
                Guide g = listGuides[i];
                listMatch.Add(ConvertSepcificGuide(g));
            }
            return listMatch;
        }

        //מקבלת מדריך ספציפי ע"פ פורמט ההשוואה של מחלקת הנירמול
        public Match GetGuideMatchDetailsByID(int id)
        {
            DBservices db = new DBservices();
            Guide g = db.GetSpecificGuideDetailsMatch(id);
            Match m = new Match();
            m = m.ConvertSepcificGuide(g);
            return m;
        }

        //מקבלת תייר ספציפי ע"פ פורמט ההשוואה של מחלקת הנירמול
        public Match GetTouristMatchDetailsByID(int id)
        {
            DBservices db = new DBservices();
            Tourist t = db.GetSpecificTouristMatchDetails(id);
            Match m = new Match();
            m = m.ConvertSpecificTourist(t);
            return m;
        }
       
        //מנרמל מדריך ספציפי לפורמט ההשוואה בין תיירים/מדריכים
        public Match ConvertSepcificGuide(Guide g)
        {
            Match m = new Match();
            m.Id = g.gCode;
            m.Rank = g.Rank;

            //הופכת תאריך לידה למספר שנה למטרת השוואת גילאים
            string o = g.BirthDay.ToString();
            char str = '/';
            string[] listTemp = o.Split(str);
            m.Age = int.Parse(listTemp[2]);
            //תאריך לידה

            //מוסיף את רשימת השפות של המדריך
            List<int> langArray = new List<int>();
            for (int i = 0; i < g.gLanguages.Count; i++)
            {
                langArray.Add(g.gLanguages[i].Language_Code);
            }
            m.Language = langArray;

            //מוסיף את רשימת התחביבים של המדריך
            List<int> HobArray = new List<int>();
            for (int i = 0; i < g.gHobbies.Count; i++)
            {
                HobArray.Add(g.gHobbies[i].HobbyHCode);
            }
            m.Hobbies = HobArray;

            //מוסיף את רשימת ההתמחויות של המדריך
            List<int> ExperArray = new List<int>();           
            for (int i = 0; i < g.gExpertises.Count; i++)
            {
                ExperArray.Add(g.gExpertises[i].ExpertiseCode);
            }
            m.Expertises = ExperArray;


            return m;
        }

        //מקבלת רשימת תיירים ע"פ הפורמט ההשוואה של מחלקת הנירמול
        public List<Match> GetTouristDetails()
        {
            DBservices db = new DBservices();
            List<Tourist> listTourist = db.GetTouristsMatchDetails();
            List<Match> listMatch = new List<Match>();
            for (int i = 0; i < listTourist.Count; i++)
            {
                Tourist t = listTourist[i];
                listMatch.Add(ConvertSpecificTourist(t));
            }
           
            return listMatch;
        }

        //מנרמלת תייר ספציפי ע"פ פורמט ההשוואה
        public Match ConvertSpecificTourist(Tourist t)
        {
            Match m = new Match();
            m.Rank = 0;
            m.Id = t.TouristID;

            //ממיר תאריך לידה לשנה על מנת להשוות בין גילאים
            string o = "";
            if (t.YearOfBirth == null)
            {
                 o = "03/03/2020";
            }
            else
            {
                 o = t.YearOfBirth;
            }
            char str = '/';
            string[] listTemp = o.Split(str);
            m.Age = int.Parse(listTemp[2]);
            //תאריך לידה
            
            //מוסיף את השפה של התייר
            List<int> langArray = new List<int>();
            if (t.LanguageCode == 0)
            {
                langArray.Add(0);
            }
            else
            {
                langArray.Add(t.LanguageCode);
            }
            m.Language = langArray;

            //מוסיף את רשימת התחביבים של התייר
            List<int> HobArray = new List<int>();
            if (t.Hobbies != null)
            {
                m.Hobbies = t.Hobbies;
            }
            else
            {
                m.Hobbies = new List<int>();
            }

            //מוסיף את רשימת ההתמחויות של התייר
            List<int> ExperArray = new List<int>();
            if (t.Expertises != null)
            {
                m.Expertises = t.Expertises;
            }
            else
            {
                m.Expertises = new List<int>();
            }
            return m;
        }
     
    }
}