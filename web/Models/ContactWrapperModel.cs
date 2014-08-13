using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class ContactWrapperModel
    {
        public SubscribeModel subcribe { get; set; }
        public Contact contact { get; set; }

        public ContactWrapperModel(Contact contact, SubscribeModel subcribe)
        {
            this.contact = contact;
            this.subcribe = subcribe;
        }
    }
}