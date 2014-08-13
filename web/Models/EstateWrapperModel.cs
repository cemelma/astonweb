using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class EstateWrapperModel
    {
        public Estate estate { get; set; }
        public List<Photo> photos { get; set; }
        public EstateWrapperModel(List<Photo> photos, Estate estate)
        {
            this.estate = estate;
            this.photos = photos;
        }
    }
}