using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{

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
        public string TripType { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string EstimateDate { get; set; }

        public string Budget { get; set; }

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

        public List<int> Interest(Tourist tourist)
        {
            List<int> touristiD_AffectedRows = new List<int>();
            int rowAffected = 0;
            string hobbiesTable = "Hobby_Tourist_Project";
            string expertisesTable = "TripPlanIntrest_Project";
            DBservices db = new DBservices();
            tourist.TouristID = db.GetTouristId(tourist);
            touristiD_AffectedRows.Add(tourist.TouristID);
            if (tourist.TouristID == 0)
            {

                return null;
            }

            for (int i = 0; i < tourist.Hobbies.Count; i++)
            {
                rowAffected += db.Interest(tourist.TouristID, tourist.Hobbies[i], hobbiesTable);
            }
            for (int i = 0; i < tourist.Expertises.Count; i++)
            {
                rowAffected += db.Interest(tourist.TouristID, tourist.Expertises[i], expertisesTable);
            }
            touristiD_AffectedRows.Add(rowAffected);
            return touristiD_AffectedRows;
        }

        public int GoogleFacebookSignUpFirstTime(Tourist tourist)
        {
            int affectedRow = 0;
            DBservices db = new DBservices();
            

            if (tourist.Email != null)
            {
                affectedRow = db.UpdateGoogleOrFacebookAccount(tourist);
                tourist.TouristID = db.GetTouristId(tourist);
                affectedRow += db.PostTouristLanguageToDB(tourist);
                if (affectedRow == 2)
                { return affectedRow; }
                else
                {
                    return 1;
                }

            }
            else
            {
                return 3;
            }

        }

        public int TouristTripType(Tourist tourist)
        {
            DBservices db = new DBservices();
            return db.TouristTripType(tourist);
        }

        public int FlightsDates(Tourist tourist)
        {
            DBservices db = new DBservices();
            return db.FlightsDatesUpdate(tourist);
        }

        public int SetBudget(Tourist tourist)
        {
            DBservices db = new DBservices();
            return db.BudgetUPDATE(tourist);
        }

        public int FirstTimeInIsraelUPDATE(Tourist tourist)
        {
            DBservices db = new DBservices();
            return db.FirstTimeInIsraelUPDATE(tourist);
        }


        //0= db error
        //2= sign up succeeded
        //3 = email already use

        public int SignUp(Tourist tourist)
        {
            int affectedRow = 0;
            DBservices db = new DBservices();
            Tourist t = db.CheckIfUserExist(tourist);

            if (t.Email == null)
            {
                affectedRow = db.SignUp(tourist);
                tourist.TouristID = db.GetTouristId(tourist);
                affectedRow += db.PostTouristLanguageToDB(tourist);
                if (affectedRow == 2)
                { return affectedRow; }
                else
                {
                    return 1;
                }

            }
            else
            {
                return 3;
            }

        }

    }
}