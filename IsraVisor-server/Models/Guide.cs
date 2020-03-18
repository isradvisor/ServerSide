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
        public string ProfilePic { get; set; }
        public string Phone { get; set; }
        public DateTime SignDate { get; set; }
        public string PasswordGuide { get; set; }
        public string Gender { get; set; }
        public object BirthDay { get; set; }
        public double Rank { get; set; }
        public int gCode { get; set; }

        public List<Guide> ReadGuides()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadGuides();
        }

      

        public int PostGuideToSQL(Guide g)
        {
            DBservices db = new DBservices();
            int numAffected = db.PostGuideToSQL(g);
            return numAffected;
        }

    
        internal int UpdateGuideSQL(Guide g)
        {
            DBservices db = new DBservices();
            int numAffected = db.UpdateGuideSQL(g);
            return numAffected;
        }

      
    }
}