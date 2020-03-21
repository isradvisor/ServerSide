using IsraVisor_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IsraVisor_server.Controllers
{
    public class LanguageController : ApiController
    {
        // GET api/<controller>
        public List<Language> Get()
        {
            Language l = new Language();
            return l.ReadFromSQL();
        }

        [HttpGet]
        [Route("api/Language/GetGuideLanguages")]
        public List<Guide_Language> GetGuideLan()
        {
            Guide_Language guideLan = new Guide_Language();
            return guideLan.ReadGuideLangsFromSQL();
        }

        // GET api/<controller>/5
        public List<Guide_Language> Get(int id)
        {
            Guide_Language g = new Guide_Language();
            return g.ReadAllGuideLanguages(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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