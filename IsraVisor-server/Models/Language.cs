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
    }
}