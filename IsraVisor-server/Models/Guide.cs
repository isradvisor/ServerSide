using System;
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
     
        public Guide GetGuideByEmail(string email)
        {
            DBservices db = new DBservices();
           return db.GetGuideByEmailFromSQL(email);
        }

        public List<Guide> ReadGuides()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadGuides();
        }

      

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

        public void ResetPassword(object email)
        {
            Guide g = new Guide();
            g = g.GetGuideByEmail(email.ToString());
            var fromAddress = new MailAddress("avielpalgi@gmail.com", "IsraVisor App");
            var toAddress = new MailAddress(email.ToString(), g.FirstName);
            const string fromPassword = "17028792";
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
        }

        public void UpdatePic(string picPath, int id)
        {
            DBservices db = new DBservices();
            db.UpdateGuidePicture(picPath, id);
        }

        public void PostPicture(object picture)
        {
            DBservices db = new DBservices();

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

        public Guide PostGuideToCheck(Guide guideCheck)
        {
            DBservices db = new DBservices();
           Guide tempGuide = db.GetGuideByEmailFromSQL(guideCheck.Email);
            if (guideCheck.FirstName == null) //check if click on sign in or other registration..
            {
                if (guideCheck.PasswordGuide == tempGuide.PasswordGuide)
                {
                    return tempGuide;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (tempGuide.Email == null)
                {
                    return PostGuideToSQL(guideCheck);
                }
                else
                {
                    return tempGuide;
                }
            }

        }
    }
}