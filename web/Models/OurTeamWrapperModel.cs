using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class OurTeamWrapperModel
    {
        public Institutional danismanlarimiz { get; set; }
        public Institutional uzmanlikalanlarimiz { get; set; }
        public IEnumerable<OurTeam> ourTeam { get; set; }

        public OurTeamWrapperModel(IEnumerable<OurTeam> ourTeam, Institutional danismanlarimiz, Institutional uzmanlikalanlarimiz)
        {
            this.danismanlarimiz = danismanlarimiz;
            this.ourTeam = ourTeam;
            this.uzmanlikalanlarimiz = uzmanlikalanlarimiz;
        }
    }
}