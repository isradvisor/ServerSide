using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsraVisor_server.Models
{
    public class Link
    {
        public int LinkCode { get; set; }
        public int guidegCode { get; set; }
        public int LinksCategoryLCode { get; set; }
        public string linkPath { get; set; }

        public List<Link> GetAllGuideLinks(int id)
        {
            DBservices db = new DBservices();
           return db.GetGuideLinksFromSQL(id);
        }

        public List<Link> UpdateLinksGuideList(List<Link> links)
        {
            DBservices db = new DBservices();
            int numAffected = 0;
            db.deleteAllGuideLinks(links[0].guidegCode);
            for (int i = 0; i < links.Count; i++)
            {
               numAffected+= db.PostGuideListLinks(links[i]);
            }
            if (numAffected != 0)
            {
                return db.GetGuideLinksFromSQL(links[0].guidegCode);
            }
            return null;
        }

        public void DeleteGuideLinks(int id)
        {
            DBservices db = new DBservices();
            db.deleteAllGuideLinks(id);
        }
    }
}