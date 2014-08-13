using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Sector
    {
        [Key]
        public int SectorId { get; set; }
        [Display(Name="Sektör İsmi")]
        [Required(ErrorMessage="Sektör İsmini Giriniz.")]
        public string Name { get; set; }
        [Display(Name = "Sektör Açıklaması")]
        public string Content { get; set; }
        [Display(Name = "Sektör Ek Doya")]
        public bool Online { get; set; }
        public int SortOrder { get; set; }
        public DateTime TimeCreated { get; set; }
        [Display(Name = "Dil")]
        [Required(ErrorMessage = "Dili Seçiniz.")]
        public string Language { get; set; }
        public string PageSlug { get; set; }
        public bool Deleted { get; set; }
        public int SectorGroupId { get; set; }
    }
}
