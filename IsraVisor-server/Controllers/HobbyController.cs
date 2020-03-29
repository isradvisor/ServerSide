using IsraVisor_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IsraVisor_server.Controllers
{
    public class HobbyController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Hobby> Get()
        {
            Hobby hobby = new Hobby();
            return hobby.GetAllHobbies();
        }

        // GET api/<controller>/5
        public List<Guide_Hobby> Get(int id)
        {
            Guide_Hobby guideHobbies = new Guide_Hobby();
            return guideHobbies.GetGuideHobbiesFromSQLBygCode(id);
        }

        // POST api/<controller>
        public List<Guide_Hobby> Post([FromBody]List<Guide_Hobby> guideHobbiesList)
        {
            Guide_Hobby guide = new Guide_Hobby();
            return guide.PostGuideListHobbies(guideHobbiesList);
        }

        // PUT api/<controller>/5
        public void Put(List<Guide_Hobby> guideHobbiesList)
        {
           
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}