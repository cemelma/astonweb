using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class SectorWrapperModel
    {
        public IEnumerable<Sector> sectors { get; set; }
        public IEnumerable<SectorGroup> sectorgroups { get; set; }
        public Sector sector { get; set; }
        public SectorGroup sectorgrp { get; set; }
        public OurSectors oursectors { get; set; }

        public SectorWrapperModel(IEnumerable<Sector> sectors, IEnumerable<SectorGroup> sectorgroups, Sector sector, OurSectors oursectors, SectorGroup sectorgrp)
        {
            this.sectors = sectors;
            this.sectorgroups = sectorgroups;
            this.sector = sector;
            this.oursectors = oursectors;
            this.sectorgrp = sectorgrp;
        }
    }

}