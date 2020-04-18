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
        // GET api/<controller> //מקבל את כל התחביבים
        public IEnumerable<Hobby> Get()
        {
            Hobby hobby = new Hobby();
            return hobby.GetAllHobbies();
        }

        // GET api/<controller>/5  //מקבל את כל התחביבם של מדריך ספציפי
        public List<Hobby> Get(int id)
        {
            Guide_Hobby guideHobbies = new Guide_Hobby();
            return guideHobbies.GetGuideHobbiesFromSQLBygCode(id);
        }

        // POST api/<controller> //מכניס את כל התחביבים של מדריך ספציפי
        public List<Hobby> Post([FromBody]List<Guide_Hobby> guideHobbiesList)
        {
            Guide_Hobby guide = new Guide_Hobby();
            return guide.PostGuideListHobbies(guideHobbiesList);
        }

        // PUT api/<controller>/5
        public void Put(List<Guide_Hobby> guideHobbiesList)
        {
           
        }

        // DELETE api/<controller>/5 //מוחק את כל התחביבים של מדריך ספציפי
        [HttpDelete]
        public void Delete(int id)
        {
            Guide_Hobby g = new Guide_Hobby();
            g.DeleteGuideHobbies(id);
        }
    }
}