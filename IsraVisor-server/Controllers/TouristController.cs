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
        //Log In Check
        // POST api/<controller>/Register
        [HttpPost]
        [Route("api/Tourist/Register")]
        public int Post2([FromBody]Tourist tourist)
        {


            return tourist.SignUp(tourist);

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