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
        public List<Hobby> Get()
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

        // POST api/<controller> //מוסיף תחביב לשרת
        [HttpPost]
        [Route("api/Hobby/AddNew")]
        public List<Hobby> PostHobby([FromBody]Hobby hobby)
        {
            Hobby hob = new Hobby();
            return hob.AddNewHobby(hobby);
        }

        // PUT api/<controller>/5
        public List<Hobby> Put(Hobby hob)
        {
            Hobby hobby = new Hobby();
            return hobby.UpdateHobby(hob);
        }

        // DELETE api/<controller>/5 //מוחק את כל התחביבים של מדריך ספציפי
        [HttpDelete]
        public List<Hobby> Delete(int id)
        {
            Guide_Hobby g = new Guide_Hobby();
           return g.DeleteGuideHobbies(id);
        }

        // DELETE api/<controller>/5 //מוחק תחביב
        [HttpDelete]
        [Route("api/Hobby/DeleteHobby")]
        public List<Hobby> Delete(Hobby hobby)
        {
            Hobby hob = new Hobby();
           return hob.DeleteHobby(hobby);
        }
    }
}