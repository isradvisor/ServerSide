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

        public List<Guide_Language> ReadGuideLangsFromSQL()
        {
            DBservices db = new DBservices();
            return db.GetGuideLangsFromSQL();
        }

        public int PostLanguagesGuideToSQL(Guide_Language guidesLanguages)
        {
            DBservices db = new DBservices();
           
            int numAffected = db.PostGuideLanguagesToSQL(guidesLanguages);
            return numAffected;
        }

        public List<Guide_Language> ReadAllGuideLanguages(int id)
        {
            DBservices db = new DBservices();
            return db.ReadGuideAllLanguagesFromSQL(id);
        }
    }
}