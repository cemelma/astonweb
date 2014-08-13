using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class ServiceModel
    {
         public Service services { get; set; }
        public List<Photo> Photos { get; set; }
       

        public ServiceModel() { }
        public ServiceModel(Service services, List<Photo> Photos)
        {
            this.services = services;
            this.Photos = Photos;
        }
    }
}