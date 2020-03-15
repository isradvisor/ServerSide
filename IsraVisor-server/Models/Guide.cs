using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class Guide
    {
        public int License { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string DescriptionGuide { get; set; }

        internal List<Guide> ReadGuides()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadGuides();
        }

        public string ProfilePic { get; set; }
        public int Phone { get; set; }
        public DateTime SignDate { get; set; }

        public int PostGuideToSQL(Guide g)
        {
            DBservices db = new DBservices();
            int numAffected = db.PostGuideToSQL(g);
            return numAffected;
        }

        internal void UpdateGuide(Guide value)
        {
            throw new NotImplementedException();
        }

        public string PasswordGuide { get; set; }
        public bool Gender { get; set; }
        public DateTime BirthDay { get; set; }
        public float Rank { get; set; }
    }
}