using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        [Display(Name="Hizmet İsmi")]
        [Required(ErrorMessage="Hizmet İsmini Giriniz.")]
        public string Name { get; set; }
        [Display(Name = "Hizmet Açıklaması")]
        public string Content { get; set; }
        [Display(Name = "Hizmet Ek Doya")]
        public bool Online { get; set; }
        public int SortOrder { get; set; }
        public DateTime TimeCreated { get; set; }
        [Display(Name = "Dil")]
        
        public string Language { get; set; }
        public string PageSlug { get; set; }
        public bool Deleted { get; set; }
        [Display(Name = "Anasayfada Göster Ek Doya")]
        public bool ShowInMenu { get; set; }
        public bool ShowContentHome { get; set; }
        public int ServiceGroupId { get; set; }
    }
}
