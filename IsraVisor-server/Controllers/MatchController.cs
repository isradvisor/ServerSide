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
        // GET api/<controller>
        public List<Match> Get()
        {
            Match m = new Match();
            return m.GetGuidesDetails();
        }

        // GET api/<controller>/5
        public Match Get(int id)
        {
            Match m = new Match();
            return m.GetGuideMatchDetailsByID(id);
        }

        //[HttpGet]
        //[Route("api/Match/getMatch")]
        //public List<Match> GetMatch()
        //{
        //    Match m = new Match();
        //    return m.ConvertGuideToMatch();
        //}

        [HttpGet]
        [Route("api/Match/calculateGuideBetweenGuides/{id}")]
        public List<CalculateMatch> GetMatchCal(int id)
        {
            CalculateMatch m = new CalculateMatch();
            return m.CalculateMatchBetweenGuideToAllGuides(id);
        }
        [HttpGet]
        [Route("api/Match/calculateTouristBetweenGuides/{id}")]
        public List<CalculateMatch> GetMatchCalTourist(int id)
        {
            CalculateMatch m = new CalculateMatch();
            return m.CalculateMatchBetweenTouristToAllGuides(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            Match m = new Match();
            m.PostGuideMatch(value);
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