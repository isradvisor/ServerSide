﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace IsraVisor_server.Models
{
    //class guide
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
        public object SignDate { get; set; }
        public string PasswordGuide { get; set; }
        public string Gender { get; set; }
        public object BirthDay { get; set; }
        public double Rank { get; set; }
        public int gCode { get; set; }
        public List<Guide_Expertise> gExpertises { get; set; }
        public List<Guide_Hobby> gHobbies { get; set; }
        public List<Guide_Language> gLanguages { get; set; }
     

        //מקבלת מדריך ע"פ אימייל
        public Guide GetGuideByEmail(string email)
        {
            DBservices db = new DBservices();
           return db.GetGuideByEmailFromSQL(email);
        }

        //מקבלת את כל המדריכים
        public List<Guide> ReadGuides()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadGuides();
        }

      //מכניסה מדריך לSQL
        public Guide PostGuideToSQL(Guide g)
        {
            DBservices db = new DBservices();
            int numAffected = db.PostGuideToSQL(g);
            if (numAffected == 1)
            {
                return db.GetGuideByEmailFromSQL(g.Email);
            }
            return null;
        }

       

        public string SendEmail(message messageContact)
        {
            var from = new MailAddress("isradvisor@gmail.com", messageContact.Name);
            var to = new MailAddress("isradvisor@gmail.com", "IsraAdvisor App");
            const string Password = "bgroup10_50290";

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(to.Address, Password)
            };
            using (var message = new MailMessage(from, to)
            {
                Subject = messageContact.EmailFrom,
                Body = messageContact.Body
            })
            {
                smtp.Send(message);
                return "success";
            }
            // code in brackets above needed if authentication required 

            //try
            //{
            //    client.Send(message);
            //    return "success";
            //}
            //catch (SmtpException ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //    return "error";
            //}
        }

        //RESET PASSWORD
        public Guide ResetPassword(object email)
        {
            Guide g = new Guide();
            g = g.GetGuideByEmail(email.ToString());
            var fromAddress = new MailAddress("isradvisor@gmail.com", "IsraAdvisor App");
            var toAddress = new MailAddress(email.ToString(), g.FirstName);
            const string fromPassword = "bgroup10_50290";
            const string subject = "Reset Password";
            string randPass = RandomPassword();
            string temp = "Hello " + g.FirstName + " " + g.LastName + " your New Password is: " + randPass;
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
            DBservices db = new DBservices();
            db.ChangePass(randPass,g.gCode);

            g.PasswordGuide = randPass;
            g.Email = email.ToString();
            return g;
        }
        public Guide GetGuideGovIlByLicenseNum(int num)
        {
            DBservices db = new DBservices();
            Guide g = db.GetGuideByLicenseNum(num);
            if (g.Email != null)
            {
                return g;
            }
            else
            {
                return null;
            }
        }
        //מכניס מדריך מאתר משרד התיירות
        public Guide PostGuideFromGov(Guide g)
        {
            DBservices db = new DBservices();
            Guide tempGuide = db.GetGuideByLicenseNum(g.License);
            if (tempGuide.FirstName == null) //בודק אם נרשם בעבר
            {
                int num = db.PostGuideToSQLFromGovIL(g);
                if (num == 1)
                {
                    return GetGuideByEmail(g.Email);

                }
                else
                {
                    return null;
                }
            }
            else
            {
                return tempGuide;
            }
         
        }

        //מעדכנת תמונת פרופיל
        public Guide UpdatePic(string picPath, int id)
        {
            DBservices db = new DBservices();
            int num = db.UpdateGuidePicture(picPath, id);
            if (num == 1)
            {
                return db.GetGuideBygCode(id);
            }
            else
            {
                return null;
            }
        }

        public void PostPicture(object picture)
        {
            DBservices db = new DBservices();

        }

        public List<Guide> GetGuidesById(int[] id)
        {
            DBservices db = new DBservices();
            List<Guide> topThreeGuides = new List<Guide>();
            for (int i = 0; i < id.Length; i++)
            {
                topThreeGuides.Add(db.GetGuideBygCode(id[i]));
            }
            return topThreeGuides;
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

        //מעדכנת את פרטי המדריך
        public Guide UpdateGuideSQL(Guide g)
        {
            DBservices db = new DBservices();
            int numAffected = db.UpdateGuideSQL(g);
            if (numAffected == 1)
            {
                return db.GetGuideByEmailFromSQL(g.Email);
            }
            else {
                return null;
            }
        }
        //הרשמת מדריך
        public int PostGuideToCheckSignUp(Guide guideCheck)
        {
            DBservices db = new DBservices();
            Guide tempGuide = db.GetGuideByEmailFromSQL(guideCheck.Email);
            if (tempGuide.Email == guideCheck.Email)
            {

                return 2;
            }
            else
            {
             return db.PostGuideToSQL(guideCheck);
            }
        }

        //בודקת אם קיים מדריך
        public Guide PostGuideToCheck(Guide guideCheck)
        {
            DBservices db = new DBservices();
           Guide tempGuide = db.GetGuideByEmailFromSQL(guideCheck.Email);

            //check facebook google
            if (guideCheck.PasswordGuide == "NoPassword")
            {
                return tempGuide;
            }
            else
            {
                if (guideCheck.PasswordGuide == tempGuide.PasswordGuide) //בודק אם הסיסמה נכונה
                {
                    return tempGuide;
                }
                else
                {
                    return null;
                }
            }
              
            //}
            //else //אם נכנס דרך ההרשמה ולא דרך ההתחברות
            //{
            //    if (tempGuide.Email == null) //בודק אם קיים מייל
            //    {
            //        return PostGuideToSQL(guideCheck); //מכניס מדריך חדש
            //    }
            //    else
            //    {
            //        return tempGuide;
            //    }
            //}

        }
    }
}