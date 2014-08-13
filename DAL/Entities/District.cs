using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class District
    {
        [Key]
        public int Id { get; set; }
        public int TownId { get; set; }
        [Display(Name="Semt Adı")]
        [Required(ErrorMessage="Semt ismini giriniz")]
        public string Name { get; set; }
     
    }
}
