using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class ServiceSubWrapperModel
    {
        public ServiceGroup servicegroup { get; set; }
        public IEnumerable<Service> services { get; set; }

        public ServiceSubWrapperModel(IEnumerable<Service> services, ServiceGroup servicegroup)
        {
            this.services = services;
            this.servicegroup = servicegroup;
            //this.productsubgroups = productsubgroups;
        }
    }
}