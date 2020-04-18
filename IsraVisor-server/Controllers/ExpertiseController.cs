using IsraVisor_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IsraVisor_server.Controllers
{
    public class ExpertiseController : ApiController
    {
        // GET api/<controller>מקבל את רשימת ההתמחויות
        public IEnumerable<Expertise> Get()
        {
            Expertise ex = new Expertise();
            return ex.GetAllExpertises();
        }

        // GET api/<controller>/5  //מקבל התמחויות של מדריך ספציפי  
        public List<Expertise> Get(int id)
        {
            Guide_Expertise ex = new Guide_Expertise();
            return ex.GetGuideExpertises(id);
        }

        // POST api/<controller>  //מכניס התמחויות של מדריך ספציפי
        public List<Expertise> Post([FromBody]List<Guide_Expertise> ex)
        {
            Guide_Expertise gExper = new Guide_Expertise();
            return gExper.PostAllGuideExpertises(ex);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5 //מוחק התמחויות של מדריך ספציפי
        public void Delete(int id)
        {
            Guide_Expertise g = new Guide_Expertise();
            g.DeleteExpertisesGuide(id);
        }
    }
}