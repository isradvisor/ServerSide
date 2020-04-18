using IsraVisor_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IsraVisor_server.Controllers
{
    public class MatchController : ApiController
    {

        //מקבלת רשימה של כל המדריכים ע"פ הפורמט של מחלקת הנירמול
        // GET api/<controller>
        public List<Match> Get()
        {
            Match m = new Match();
            return m.GetGuidesDetails();
        }

        //מקבלת מדריך ספציפי ע"פ הפורמט של מחלקת הנירמול
        // GET api/<controller>/5
        public Match Get(int id)
        {
            Match m = new Match();
            return m.GetGuideMatchDetailsByID(id);
        }

        //עושה השוואה בין מדריך לכל המדריכים
        [HttpGet]
        [Route("api/Match/calculateGuideBetweenGuides/{id}")]
        public List<CalculateMatch> GetMatchCal(int id)
        {
            CalculateMatch m = new CalculateMatch();
            return m.CalculateMatchBetweenGuideToAllGuides(id);
        }

        //עושה השוואה בין תייר לכל המדריכים על ידי מספר איידי של תייר
        [HttpGet]
        [Route("api/Match/calculateTouristBetweenGuides/{id}")]
        public List<CalculateMatch> GetMatchCalTourist(int id)
        {
            CalculateMatch m = new CalculateMatch();
            return m.CalculateMatchBetweenTouristToAllGuides(id);
        }

        //עושה השוואה בין תייר לכל התיירים ע"י מספר איידי של תייר
        [HttpGet]
        [Route("api/Match/calculateTouristBetweenTourists/{id}")]
        public List<CalculateMatch> GetMatchCalTouristBetweenTourist(int id)
        {
            CalculateMatch m = new CalculateMatch();
            return m.CalculateMatchBetweenTouristToAllTourists(id);
        }

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //    Match m = new Match();
        //    m.PostGuideMatch(value);
        //}

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