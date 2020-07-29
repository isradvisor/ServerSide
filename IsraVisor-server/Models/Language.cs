using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string LName { get; set; }
        public string LNameEnglish { get; set; }
        public int LCode { get; set; }
        //מקבלת את כל השפות מהSQL
        public List<Language> ReadFromSQL()
        {
            DBservices db = new DBservices();
           return db.ReadLanguagesFromSQL();
        }

        public List<Language> AddNewLanguage(Language lang)
        {
            DBservices db = new DBservices();
            if (db.AddLanguageToSQL(lang) == 1)
            {
                return db.ReadLanguagesFromSQL();
            }
            else
            {
                return null;
            }
        }

        public List<Language> UpdateLanguage(Language lang)
        {
            DBservices db = new DBservices();
            if (db.UpdateLanguage(lang) == 1)
            {
                return db.ReadLanguagesFromSQL();
            }
            else
            {
                return null;
            }
        }

        public List<Language> DeleteLanguage(Language lang)
        {
            DBservices db = new DBservices();
            int num = db.ReadLanguagesFromSQL().Count;
            db.deleteLanguageFromSQL(lang);
            List<Language> languages = db.ReadLanguagesFromSQL();
            if (languages.Count+1 == num)
            {
                return languages;
            }
            else
            {
                return null;
            }
        }
    }
}