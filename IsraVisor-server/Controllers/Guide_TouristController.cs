using IsraVisor_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IsraVisor_server.Controllers
{
    public class Guide_TouristController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5  //מקבל את כל הניקודים שהמדריך קיבל מהתיירים
        public double Get(int id)
        {
            Guide_Tourist g = new Guide_Tourist();
           return g.GetAllRanksOfGuide(id);
        }

        // POST api/<controller> //מכניס ניקוד של תייר לSQL 
        public void Post([FromBody]Guide_Tourist guide_Tourist)
        {
            Guide_Tourist g = new Guide_Tourist();
            g.PostGuideTouristRank(guide_Tourist);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}