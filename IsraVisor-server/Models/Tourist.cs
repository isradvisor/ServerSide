using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class Tourist
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordTourist { get; set; }
        public bool Gender { get; set; }
        public DateTime YearOfBirth { get; set; }
        public string ProfilePic { get; set; }
        public string InterestGender { get; set; }
        public bool FirstTimeInIsrael { get; set; }
        public int LanguageCode { get; set; }

        public List<Tourist> readTourist()
        {
            DBservices dbs = new DBservices();
            return dbs.readTourist();
        }

      

        public int InsertTourist(Tourist tourist)
        {
            DBservices db = new DBservices();
            return db.PostTouristToSQL(tourist);
        }

    }
}