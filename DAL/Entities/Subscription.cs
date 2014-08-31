using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Subscription
    {
        [Key]
        public int SubscriptionId { get; set; }

        public string Email { get; set; }

        public DateTime CreateTime { get; set; }

    }
}
