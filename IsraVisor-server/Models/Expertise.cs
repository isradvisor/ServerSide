using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class Expertise
    {
        public int Code { get; set; }
        public string NameE { get; set; }
        public string Picture { get; set; }
        public string Type { get; set; }

        //מקבלת את כל ההתמחויות מהSQL
        public List<Expertise> GetAllExpertises()
        {
            DBservices db = new DBservices();
            return db.GetAllExpertisesFromSQL();
        }

        public List<Expertise> AddNewExpertise(Expertise exper)
        {
            DBservices db = new DBservices();
            if (db.AddNewExpertise(exper)==1)
            {
                return db.GetAllExpertisesFromSQL();
            }
            else
            {
                return null;
            }
        }

        public List<Expertise> UpdateExpertise(Expertise ex)
        {
            DBservices db = new DBservices();
            if (db.UpdateExpertise(ex) == 1)
            {
                return db.GetAllExpertisesFromSQL();
            }
            else
            {
                return null;
            }
        }

        public List<Expertise> DeleteExpertise(Expertise exp)
        {
            DBservices db = new DBservices();
            int num = db.GetAllExpertisesFromSQL().Count;
            db.DeleteExpertiseFromSQL(exp);
            List<Expertise> expertises = db.GetAllExpertisesFromSQL();
            if (expertises.Count+1 == num)
            {
                return expertises;
            }
            else
            {
                return null;
            }
        }
    }
}