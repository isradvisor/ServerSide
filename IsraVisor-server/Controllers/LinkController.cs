using IsraVisor_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IsraVisor_server.Controllers
{
    public class LinkController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public List<Link> Get(int id)
        {
            Link l = new Link();
            return l.GetAllGuideLinks(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        [HttpPut]
        [Route("api/Link/UpdateLinks")]
        public List<Link> Put([FromBody]List<Link> links)
        {
            Link l = new Link();
           return l.UpdateLinksGuideList(links);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(int id)
        {
            Link l = new Link();
            l.DeleteGuideLinks(id);
        }
    }
}