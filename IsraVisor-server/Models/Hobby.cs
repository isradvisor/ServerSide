using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class Hobby
    {
        public string HName { get; set; }
        public string Picture { get; set; }

        public IEnumerable<Hobby> GetAllHobbies()
        {
            DBservices db = new DBservices();
            return db.GetAllHobbiesFromSQL();
        }
    }
}