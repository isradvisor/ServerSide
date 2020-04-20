using IsraVisor_server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace IsraVisor_server.Controllers
{
    public class GuideController : ApiController
    {
        // GET api/<controller>מקבלת את כל המדריכים
        public List<Guide> Get()
        {
            Guide g = new Guide();
            return g.ReadGuides();
        }

        // GET api/<controller>/5 //מקבלת מדריך ע"פ אימייל
        public Guide Get(string email)
        {
            Guide g = new Guide();
           return g.GetGuideByEmail(email);
        }

     
        // POST api/<controller>//מכניסה מדריך לSQL
        public Guide Post([FromBody]Guide g)
        {
            Guide g1 = new Guide();
           return g1.PostGuideToSQL(g);
        }

        //מכניסה את שפות המדריך
        [HttpPost]
        [Route("api/Guide/PostGuideLanguage")]
        public List<Guide_Language> PostLanguage([FromBody]List<Guide_Language> guideLan)
        {
            Guide_Language guideLnaguage = new Guide_Language();
            return guideLnaguage.PostGuideLanguagesToSQL(guideLan);
        }

        //Post To Check log in // בודק אם משתמש קיים
        [HttpPost]
        [Route("api/Guide/PostToCheck")]
        public Guide CheckPost([FromBody]Guide guideCheck)
        {
            Guide checkGuide = new Guide();
            return checkGuide.PostGuideToCheck(guideCheck);
        }

        //RESET PASSWORD
        [HttpPost]
        [Route("api/Guide/Reset")]
        public void Reset([FromBody]object email)
        {
            Guide checkGuide = new Guide();
             checkGuide.ResetPassword(email);
        }


        [HttpPost]
        [Route("api/Guide/UpdateProfilePic")]
        public Guide PostPic(ProfilePicture GuidepicAndId)
        {
            Guide g = new Guide();
           return g.UpdatePic(GuidepicAndId.picPath, GuidepicAndId.id);
        }

        //מכניס מדריך מאתר משרד התיירות
        [HttpPost]
        [Route("api/Guide/PostGuideFromGovIL")]
        public Guide PostGuideFromGov(Guide g)
        {
            Guide guide = new Guide();
           return guide.PostGuideFromGov(g);
        }
        [HttpPost]
        [Route("api/Guide/PostPic")]
        public HttpResponseMessage Post()
        {
            List<string> imageLinks = new List<string>();
            var httpContext = HttpContext.Current;

            // Check for any uploaded file  
            if (httpContext.Request.Files.Count > 0)
            {
                //Loop through uploaded files  
                for (int i = 0; i < httpContext.Request.Files.Count; i++)
                {
                    HttpPostedFile httpPostedFile = httpContext.Request.Files[i];

                    // this is an example of how you can extract addional values from the Ajax call
                    string name = httpContext.Request.Form["user"];

                    if (httpPostedFile != null)
                    {
                        // Construct file save path  
                        //var fileSavePath = Path.Combine(HostingEnvironment.MapPath(ConfigurationManager.AppSettings["fileUploadFolder"]), httpPostedFile.FileName);
                        string fname = httpPostedFile.FileName.Split('\\').Last();
                        var fileSavePath = Path.Combine(HostingEnvironment.MapPath("~/uploadedFiles"), fname);
                        // Save the uploaded file  
                        httpPostedFile.SaveAs(fileSavePath);
                        imageLinks.Add("uploadedFiles/" + fname);
                    }
                }
            }
           
            // Return status code  
            return Request.CreateResponse(HttpStatusCode.Created, imageLinks);
        }

        //Get from DB top 3 guides that matched 
        [HttpPost]
        [Route("api/Guide/TopMatchGuides")]
        public List<Guide> TopMatchGuides(int [] id)
        {
            Guide g = new Guide();
            return g.GetGuidesById(id);
        }

        // PUT api/<controller>/5 מעדכנת פרטי המדריך
        public Guide Put([FromBody]Guide g)
        {
            Guide guide = new Guide();
            return(guide.UpdateGuideSQL(g));
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}