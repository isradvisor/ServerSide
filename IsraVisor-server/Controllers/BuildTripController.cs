using IsraVisor_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IsraVisor_server.Controllers
{
    public class BuildTripController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Guide_Tourist> Get()
        {
            Guide_Tourist gt = new Guide_Tourist();
            return gt.GetAllRequests();
        }
        // GET api/<controller>/5
        [HttpGet]
        [Route("api/BuildTrip/GetRequests")]
        public List<Guide_Tourist> GetRequests(string email)
        {
            Guide_Tourist gt = new Guide_Tourist();
            return gt.GetRequestsFromSQL(email);
        }

        [HttpGet]
        [Route("api/BuildTrip/GetTouristStatus")]
        public Guide_Tourist GetTouristStatus(string email)
        {
            Guide_Tourist gt = new Guide_Tourist();
            return gt.GetTouristStatus(email);
        }


        // GET api/<controller>/5
        [HttpGet]
        [Route("api/BuildTrip/GetCities/{id}")]
        public List<AreaPointInTrip> GetCities(int id)
        {
            AreaPointInTrip cities = new AreaPointInTrip();
            return cities.GetCitiesByIdPlan(id);
        }
        [HttpGet]
        [Route("api/BuildTrip/GetAttractions/{id}")]
        public List<AttractionPointInTrip> GetAttractions(int id)
        {
            AttractionPointInTrip attractions = new AttractionPointInTrip();
            return attractions.GetAttractionsById(id);
        }
        [HttpGet]
        [Route("api/BuildTrip/GetAllAttractions")]
        public List<Attraction> GetAllAttractions()
        {
            Attraction att = new Attraction();
            return att.GetAllAttractionsFromSQL();
        }
        [HttpPost]
        [Route("api/BuildTrip/AddAttraction")]
        public AttractionPointInTrip PostAttraction(AttractionPointInTrip attraction)
        {
            AttractionPointInTrip a = new AttractionPointInTrip();
            return a.AddAttractionToSQL(attraction);
        }
        [HttpPost]
        [Route("api/BuildTrip/AddAllAtractions")]
        public int postAllAttractions(List<Attraction> attractions)
        {
            Attraction att = new Attraction();
           return att.postAllAtractionsToSQL(attractions);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }
        
        // GET api/<controller>?email=aaa
        public List<TripPoint> Get(string email)
        {
            TripPoint point = new TripPoint();
            return point.GetAllPointsFromSQL(email);
        }

        [HttpPost]
        [Route("api/BuildTrip/AddRequest")]
        public int PostRequest(Guide_Tourist gt)
        {
            Guide_Tourist g = new Guide_Tourist();
            return g.AddRequest(gt);
        }


        //public void Timer()
        //{
        //    var Startimer = TimeSpan.Zero;
        //    var EndTimer = TimeSpan.FromSeconds(3);
        //    var timer = new System.Threading.Timer((e) =>
        //    {
        //        string email = "";
        //        Get(email);
        //    });
        //}

        // POST api/<controller>
        public List<TripPoint> Post([FromBody] List<TripPoint> tripPoints )
        {
            TripPoint point = new TripPoint();
          return point.AddTripToTourist(tripPoints);
        }

        // PUT api/<controller>/5
        public List<Guide_Tourist> Put(Guide_Tourist g)
        {
            Guide_Tourist gt = new Guide_Tourist();
           return gt.UpdateRequest(g);
        }

        private void UpdateRequest(Guide_Tourist g)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}