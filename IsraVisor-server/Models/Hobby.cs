using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class Hobby
    {
        public int HCode { get; set; }
        public string HName { get; set; }
        public string Picture { get; set; }
        
        //מקבלת את כל התחביבים מהSQL
        public IEnumerable<Hobby> GetAllHobbies()
        {
            DBservices db = new DBservices();
            return db.GetAllHobbiesFromSQL();
        }
    }
}