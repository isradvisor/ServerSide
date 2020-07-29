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
        //GET api/<controller>  GET ALL Requests 
        public IEnumerable<Guide_Tourist> Get()
        {
            Guide_Tourist gt = new Guide_Tourist();
            return gt.GetAllRequests();
        }

        // Get All Request by email guide
        [HttpGet]
        [Route("api/BuildTrip/GetRequests")]
        public List<Guide_Tourist> GetRequests(string email)
        {
            Guide_Tourist gt = new Guide_Tourist();
            return gt.GetRequestsFromSQL(email);
        }

        //Get all Tourist status = 'Start Chat'
        [HttpPost]
        [Route("api/BuildTrip/GetAllListTouristsStatus")]
        public List<Guide_Tourist> GetAllListStatus(List<Tourist> Tourists)
        {
            Guide_Tourist gt = new Guide_Tourist();
           return gt.GetAllListStatus(Tourists);
        }

        //Get Tourist's status by tourist email
        [HttpGet]
        [Route("api/BuildTrip/GetTouristStatus")]
        public Guide_Tourist GetTouristStatus(string email)
        {
            Guide_Tourist gt = new Guide_Tourist();
            return gt.GetTouristStatus(email);
        }
      
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        //// GET api/<controller>?email=aaa    Get all points of trip by tourist email
        public List<TripPoint> Get(string email)
        {
            TripPoint point = new TripPoint();
            return point.GetAllPointsFromSQL(email);
        }

        //Add status request
        [HttpPost]
        [Route("api/BuildTrip/AddRequest")]
        public int PostRequest(Guide_Tourist gt)
        {
            Guide_Tourist g = new Guide_Tourist();
            return g.AddRequest(gt);
        }

        //Add tourist trip
        [HttpPost]
        [Route("api/BuildTrip/GetAtt")]
        public List<TripPoint> PutPoint(List<TripPoint> tripPoints)
        {
            TripPoint point = new TripPoint();
            return point.AddTripToTourist(tripPoints);

        }

        // PUT api/<controller>/5   Update tourist request
        public List<Guide_Tourist> Put(Guide_Tourist g)
        {
            Guide_Tourist gt = new Guide_Tourist();
            return gt.UpdateRequest(g);
        }


        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

    }
}