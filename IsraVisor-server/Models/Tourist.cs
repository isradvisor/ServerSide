﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
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
        public string Token { get; set; }
        public string SecondEmail { get; set; }

        //public List<Tourist> readAllTourist()
        public Tourist GetUserDetails(string email)
        {
            DBservices db = new DBservices();
            return db.GetAllDetailsFromSQL(email);
        }

      
        public int LanguageCode { get; set; }
        public string LNameEnglish { get; set; }
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
            Tourist t =  db.LogInCheck(tourist);
            if (t.FirstName != null)
            {
                return db.GetAllDetailsFromSQL(t.Email);
            }
            else {
                return null;
            }

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
        //1= update succeeded
        public int EditProfile (Tourist tourist)
        {
            DBservices db = new DBservices();
            return db.EditProfile(tourist);
        }

        //0= db error
        //1= update succeeded
        public int UploadPicture(Tourist tourist)
        {
            DBservices db = new DBservices();
            return db.UploadPicture(tourist);
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

        //public Tourist GetAllDetails(string email)
       

        //public Tourist GetAllDetails(int id)
        //{
        //    DBservices db = new DBservices();
        //    return db.GetAllDetailsFromSQL(email);
        //}

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

        public PasswordReset ResetPassword(string email)
        {
            DBservices db = new DBservices();
            Tourist t = new Tourist();
            t = (db.GetAllDetailsFromSQL(email));
            PasswordReset pr = new PasswordReset();
            pr.TouristEmail = t.Email;
            pr.OldPassword = t.PasswordTourist;
            if (t.Email != null)
            {
                var fromAddress = new MailAddress("isradvisor@gmail.com", "IsraAdvisor App");
                var toAddress = new MailAddress(email.ToString(), t.FirstName);
                const string fromPassword = "bgroup10_50290";
                const string subject = "Reset Password";
                string randPass = RandomPassword();
                string temp = "Hello " + t.FirstName + " " + t.LastName + " your New Password is: " + randPass;
                string body = temp;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                int num = db.ChangeTouristPassword(randPass, t.Email);
                if (num == 1)
                {
                    pr.NewPassword = randPass;
                    return pr;
                }
                else
                {
                    return null;
                }
                // db.ChangePass(randPass, g.gCode);
            }
            else {
                return null;
            }

        }

        // Generate a random password of a given length (optional)  
        public string RandomPassword(int size = 10)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        // Generate a random number between two numbers    
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        // Generate a random string with a given size and case.   
        // If second parameter is true, the return string is lowercase  
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public int TouristTripType(Tourist tourist)
        {
            DBservices db = new DBservices();

            if (db.GetTouristtripPlan(tourist).Email == null)
            {
                return db.TouristTripType(tourist);
            }
            else
            {
                return db.updateTripType(tourist);
            }
          
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