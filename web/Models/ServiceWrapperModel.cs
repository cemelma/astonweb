using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class ServiceWrapperModel
    {
        public IEnumerable<Service> services { get; set; }
        public IEnumerable<ServiceGroup> servicegroups { get; set; }
        public Service service { get; set; }
        public OurServices ourservices { get; set; }
        public ServiceGroup servivegrp { get; set; }

        public ServiceWrapperModel(ServiceGroup servivegrp, IEnumerable<Service> services, IEnumerable<ServiceGroup> servicegroups, Service service, OurServices ourservices)
        {
            this.services = services;
            this.servicegroups = servicegroups;
            this.service = service;
            this.ourservices = ourservices;
            this.servivegrp = servivegrp;
        }
    }

}