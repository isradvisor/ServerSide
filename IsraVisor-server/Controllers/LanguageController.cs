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
        // GET api/<controller> //מקבל את כל רשימת השפות
        public List<Language> Get()
        {
            Language l = new Language();
            return l.ReadFromSQL();
        }

        // GET api/<controller>/5 //מקבלת רשימת שפות של מדריך ספציפי
        public List<Guide_Language> Get(int id)
        {
            Guide_Language g = new Guide_Language();
            return g.ReadAllGuideLanguages(id);
        }

        // POST api/<controller>
        public List<Language> Post([FromBody]Language lang)
        {
            Language ln = new Language();
            return ln.AddNewLanguage(lang);
        }

        // PUT api/<controller>/5
        public List<Language> Put(Language lang)
        {
            Language lng = new Language();
            return lng.UpdateLanguage(lang);
        }

        // DELETE: api/Student?id=5 //מוחקת רשימת שפות של מדריך ספציפי
        [HttpDelete]
        public void Delete(int id)
        {
            Guide_Language g = new Guide_Language();
            g.DeleteGuideLanguages(id);
        }

        // DELETE: api/Student?id=5 //מוחקת רשימת שפות של מדריך ספציפי
        [HttpDelete]
        [Route("api/Language/DeleteLang")]
        public List<Language> DeleteLanguage(Language lang)
        {
            Language lng = new Language();
            return lng.DeleteLanguage(lang);
        }
    }
}