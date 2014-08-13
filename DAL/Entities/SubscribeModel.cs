using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class SubscribeModel
    {
        //model specific fields 
        [Required]
        [Display(Name = "Toplam Nedir?")]
        public string Captcha { get; set; } 
    }
}
