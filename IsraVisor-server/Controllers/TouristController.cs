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

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
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

        //First Time in Israel Screen UPDATE (true or false)
        // PUT api/<controller>/Register
        [HttpPut]
        [Route("api/Tourist/FirstTimeInIsrael")]
        public int Put([FromBody]Tourist tourist)
        {
            return tourist.FirstTimeInIsraelUPDATE(tourist);
        }
       
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}