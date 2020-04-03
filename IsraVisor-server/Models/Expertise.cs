using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class Expertise
    {
        public int Code { get; set; }
        public string NameE { get; set; }
        public string Picture { get; set; }

        public IEnumerable<Expertise> GetAllExpertises()
        {
            DBservices db = new DBservices();
            return db.GetAllExpertisesFromSQL();
        }
    }
}