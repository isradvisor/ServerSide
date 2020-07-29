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
        public List<Expertise> Get()
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

        // POST api/<controller>  //מוסיף התמחות לשרת
        [HttpPost]
        [Route("api/Expertise/AddNew")]
        public List<Expertise> PostExpertise([FromBody]Expertise exper)
        {
            Expertise ex = new Expertise();
            return ex.AddNewExpertise(exper);
        }

        // PUT api/<controller>/5
        public List<Expertise> Put(Expertise ex)
        {
            Expertise expertise = new Expertise();
            return expertise.UpdateExpertise(ex);
        }

        // DELETE api/<controller>/5 //מוחק התמחויות של מדריך ספציפי
        [HttpDelete]
        public void Delete(int id)
        {
            Guide_Expertise g = new Guide_Expertise();
            g.DeleteExpertisesGuide(id);
        }

        // DELETE  //מוחק התמחות
        [HttpDelete]
        [Route("api/Expertise/DeleteExpertise")]
        public List<Expertise> DeleteExpertise(Expertise exp)
        {
            Expertise exer = new Expertise();
           return exer.DeleteExpertise(exp);
        }
    }
}