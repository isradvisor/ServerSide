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
            List<CalculateMatch> allGuidesWithPercents = m.CalculateMatchBetweenTouristToAllGuides(id);
            List<CalculateMatch> topThreeMatchesGuides = new List<CalculateMatch>();
            object[] first = new object[2];
            object[] second = new object[2];
            object[] third = new object[2];

            //the first elment in each array is the match percentage, 
            //and the second element is a CalculateMatch obejct that include the ID of the guide

            third[0] = first[0] = second[0] = 000;
            
            for (int i = 0; i < allGuidesWithPercents.Count; i++)
            {
                // If current element is  
                // greater than first 
                if (allGuidesWithPercents[i].Percents > Convert.ToDouble(first[0]))
                {
                    third[0] = second[0];
                    third[1] = second[1];
                    second[0] = first[0];
                    second[1] = first[1];
                    first[0] = allGuidesWithPercents[i].Percents;
                    first[1] = allGuidesWithPercents[i];
                }

                // If arr[i] is in between first 
                // and second then update second 
                else if (allGuidesWithPercents[i].Percents > Convert.ToDouble(second[0]))
                {
                    third[0] = second[0];
                    third[1] = second[1];
                    second[0] = allGuidesWithPercents[i].Percents;
                    second[1] = allGuidesWithPercents[i];
                }

                else if (allGuidesWithPercents[i].Percents > Convert.ToDouble(third[0]))
                {
                    third[0] = allGuidesWithPercents[i].Percents;
                    third[1] = allGuidesWithPercents[i];
                }
            }
            topThreeMatchesGuides.Add((CalculateMatch)first[1]);
            topThreeMatchesGuides.Add((CalculateMatch)second[1]);
            topThreeMatchesGuides.Add((CalculateMatch)third[1]);
            return topThreeMatchesGuides;

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