using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class Hobby
    {
        public int HCode { get; set; }
        public string HName { get; set; }
        public string Picture { get; set; }
        public string Type { get; set; }

        //מקבלת את כל התחביבים מהSQL
        public List<Hobby> GetAllHobbies()
        {
            DBservices db = new DBservices();
            return db.GetAllHobbiesFromSQL();
        }

        public List<Hobby> AddNewHobby(Hobby hobby)
        {
            DBservices db = new DBservices();
            int num = db.AddNewHobby(hobby);
            if (num == 1)
            {
                return db.GetAllHobbiesFromSQL();
            }
            else
            {
                return null;
            }
        }

        public List<Hobby> UpdateHobby(Hobby hob)
        {
            DBservices db = new DBservices();
            int num = db.UpdateHobby(hob);
            if (num == 1)
            {
                return db.GetAllHobbiesFromSQL();
            }
            else
            {
                return null;
            }
        }

        public List<Hobby> DeleteHobby(Hobby hobby)
        {
            DBservices db = new DBservices();
            int num1 = db.GetAllHobbiesFromSQL().Count;
            db.DeleteHobby(hobby);
            List<Hobby> hobbies = db.GetAllHobbiesFromSQL();
            if (hobbies.Count + 1 == num1)
            {
                return hobbies;
            }
            else {
                return null;
            }
        }
    }
}