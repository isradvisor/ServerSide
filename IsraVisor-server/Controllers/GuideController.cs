﻿using IsraVisor_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IsraVisor_server.Controllers
{
    public class GuideController : ApiController
    {
        // GET api/<controller>
        public List<Guide> Get()
        {
            Guide g = new Guide();
            return g.ReadGuides();
        }

        // GET api/<controller>/5
        public Guide Get(string email)
        {
            Guide g = new Guide();
           return g.GetGuideByEmail(email);
        }

     
        // POST api/<controller>
        public Guide Post([FromBody]Guide g)
        {
            Guide g1 = new Guide();
           return g1.PostGuideToSQL(g);
        }

        [HttpPost]
        [Route("api/Guide/PostGuideLanguage")]
        public List<Guide_Language> PostLanguage([FromBody]List<Guide_Language> guideLan)
        {
            Guide_Language guideLnaguage = new Guide_Language();
            return guideLnaguage.PostGuideLanguagesToSQL(guideLan);
            //guideLnagu.PostLanguagesGuideToSQL(guideLan);
        }
        //Post To Check log in
        [HttpPost]
        [Route("api/Guide/PostToCheck")]
        public Guide CheckPost([FromBody]Guide guideCheck)
        {
            Guide checkGuide = new Guide();
            return checkGuide.PostGuideToCheck(guideCheck);
        }
        [HttpPost]
        [Route("api/Guide/Reset")]
        public void Reset([FromBody]object email)
        {
            Guide checkGuide = new Guide();
             checkGuide.ResetPassword(email);
        }

        // PUT api/<controller>/5
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