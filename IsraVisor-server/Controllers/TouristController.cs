using IsraVisor_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IsraVisor_server.Controllers
{
    public class TouristController : ApiController
    {
        // GET api/<controller>
        public List<Tourist> Get()
        {
            Tourist t = new Tourist();
            return t.readTourist();
        }



        //Log In Check
        // POST api/<controller>
        public Tourist Post([FromBody]Tourist tourist)
        {

            Tourist t = new Tourist();
            return t.LogIn(tourist);

        }
        //Sign In
        // POST api/<controller>/Register
        [HttpPost]
        [Route("api/Tourist/Register")]
        public int Post2([FromBody]Tourist tourist)
        {


            return tourist.SignUp(tourist);

        }
        //Sign In with facebook
        // POST api/<controller>/FacebookUser
        [HttpPost]
        [Route("api/Tourist/FacebookUser")]
        public int Post3([FromBody]Tourist tourist)
        {


            return tourist.SignUpFacebook(tourist);

        }
        //Sign In with google
        // POST api/<controller>/GoogleUser
        [HttpPost]
        [Route("api/Tourist/GoogleUser")]
        public int Post4([FromBody]Tourist tourist)
        {


            return tourist.SignUpGoogle(tourist);

        }

        //What is your Trip Type Screen
        // POST api/<controller>/TripType
        [HttpPost]
        [Route("api/Tourist/TripType")]
        public int Post5([FromBody]Tourist tourist)
        {


            return tourist.TouristTripType(tourist);

        }

        //What is the user's interest (expertises & hobbies)
        // POST api/<controller>/Interest
        [HttpPost]
        [Route("api/Tourist/Interest")]
        public List<int> Post6([FromBody]Tourist tourist)
        {


            return tourist.Interest(tourist);

        }

        //First Time in Israel Screen UPDATE (true or false)
        // PUT api/<controller>/Register
        [HttpPut]
        [Route("api/Tourist/FirstTimeInIsrael")]
        public int Put([FromBody]Tourist tourist)
        {
            return tourist.FirstTimeInIsraelUPDATE(tourist);
        }

        //Flights Dates UPDATE (FromDate, EndDate / EstimateDate)
        // PUT api/<controller>/FlightsDates
        [HttpPut]
        [Route("api/Tourist/FlightsDates")]
        public int Put2([FromBody]Tourist tourist)
        {
            return tourist.FlightsDates(tourist);
        }

        //Budget UPDATE (string)
        // PUT api/<controller>/Budget
        [HttpPut]
        [Route("api/Tourist/Budget")]
        public int Put3([FromBody]Tourist tourist)
        {
            return tourist.SetBudget(tourist);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}