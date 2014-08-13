using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class SectorSubWrapperModel
    {
        public SectorGroup sectorgroup { get; set; }
        public IEnumerable<Sector> sectors { get; set; }

        public SectorSubWrapperModel(IEnumerable<Sector> sectors, SectorGroup sectorgroup)
        {
            this.sectors = sectors;
            this.sectorgroup = sectorgroup;
            //this.productsubgroups = productsubgroups;
        }
    }
}