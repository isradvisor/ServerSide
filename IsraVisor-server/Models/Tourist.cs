using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    //aviel
    public class Tourist
    {
        public int TouristID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordTourist { get; set; }
        public string Gender { get; set; }
        public string YearOfBirth { get; set; }
        public string ProfilePic { get; set; }
        public string InterestGender { get; set; }
        public bool FirstTimeInIsrael { get; set; }
        public int LanguageCode { get; set; }
        public List<int> Hobbies { get; set; }
        public List<int> Expertises { get; set; }
        public List<Tourist> readTourist()
        {
            DBservices dbs = new DBservices();
            return dbs.readTourist();
        }



        public Tourist LogIn(Tourist tourist)
        {
            DBservices db = new DBservices();
            return db.LogInCheck(tourist);
        }



        public int InsertTourist(Tourist tourist)
        {
            DBservices db = new DBservices();
            return db.PostTouristToSQL(tourist);
        }


        //0= db error
        //1= sign up succeeded
        //2 = email already use
        public int SignUpFacebook(Tourist tourist)
        {
            DBservices db = new DBservices();
            Tourist t = db.LogInFacebook(tourist);
            if (t.Email == null)
            {
                return db.AddFacebookAccount(tourist);
            }
            else
            {
                return 2;
            }
        }
        //
        public int SignUpGoogle(Tourist tourist)
        {
            DBservices db = new DBservices();
            Tourist t = db.LogInFacebook(tourist);
            if (t.Email == null)
            {
                return db.AddGoogleAccount(tourist);
            }
            else
            {
                return 2;
            }
        }


        //0= db error
        //1= sign up succeeded
        //2 = email already use

        public int SignUp(Tourist tourist)
        {
            DBservices db = new DBservices();
            Tourist t = LogIn(tourist);
            if (t.Email == null)
            {
                return db.SignUp(tourist);
            }
            else
            {
                return 2;
            }

        }

    }
}