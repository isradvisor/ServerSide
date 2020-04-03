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
        public List<Hobby> Get(int id)
        {
            Guide_Hobby guideHobbies = new Guide_Hobby();
            return guideHobbies.GetGuideHobbiesFromSQLBygCode(id);
        }

        //[HttpGet]
        //[Route("api/Hobby/GetHobbiesNotSelect/id")]
        //public List<Hobby> GetHobbiesNotSelect(int id)
        //{
        //    Guide_Hobby g = new Guide_Hobby();
        //   //return g.GetHobbiesNotSelected(id);
        //}
        // POST api/<controller>
        public List<Hobby> Post([FromBody]List<Guide_Hobby> guideHobbiesList)
        {
            Guide_Hobby guide = new Guide_Hobby();
            return guide.PostGuideListHobbies(guideHobbiesList);
        }

        // PUT api/<controller>/5
        public void Put(List<Guide_Hobby> guideHobbiesList)
        {
           
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(int id)
        {
            Guide_Hobby g = new Guide_Hobby();
            g.DeleteGuideHobbies(id);
        }
    }
}