using IsraVisor_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IsraVisor_server.Controllers
{
    public class AreaController : ApiController
    {
        // GET api/<controller>
        public List<Area> Get()
        {

            Area area = new Area();
          return area.GetAllCitiesFromSQL();
           
        }

        // GET api/<controller>/5
        public List<Guide_Area> Get(int id)
        {
            Guide_Area GuideAreas = new Guide_Area();
            return GuideAreas.ReadAllAreasByGuide(id);
        }

        // POST api/<controller>
        public void Post([FromBody]List<Area> AllCities)
        {
            Area area = new Area();
            area.PostAllCitiesToSQL(AllCities);
        }

        [HttpPost]
        [Route("api/Area/PostGuideAreas")]
        public List<Guide_Area> PostLanguage([FromBody]List<Guide_Area> guide_AreasList)
        {
            Guide_Area Guide_Area = new Guide_Area();
            return Guide_Area.PostGuideAreasToSQL(guide_AreasList);
        }


        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(int id)
        {
            Guide_Area ga = new Guide_Area();
            ga.DeleteAllguideAreaList(id);
        }
    }
}