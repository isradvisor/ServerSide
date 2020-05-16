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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
        public List<AttractionPointInTrip> GetCities(int id)
        {
            AttractionPointInTrip attractions = new AttractionPointInTrip();
            return attractions.GetAttractionsById(id);
        }
        [HttpPost]
        [Route("api/BuildTrip/AddAttraction")]
        public AttractionPointInTrip PostAttraction(AttractionPointInTrip attraction)
        {
            AttractionPointInTrip a = new AttractionPointInTrip();
            return a.AddAttractionToSQL(attraction);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public AreaPointInTrip Post([FromBody] AreaPointInTrip trip )
        {
            AreaPointInTrip area = new AreaPointInTrip();
          return area.AddTripToTourist(trip);
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