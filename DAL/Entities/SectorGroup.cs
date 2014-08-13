using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class SectorGroup
    {
        [Key]
        public int SectorGroupId { get; set; }

        [Required(ErrorMessage = "Grup Adını Giriniz")]
        [Display(Name = "Grup Adı")]
        public string GroupName { get; set; }

        public bool Online { get; set; }
        public bool Deleted { get; set; }
       
        public Nullable<DateTime> TimeCreated { get; set; }

        [Required(ErrorMessage = "Dili Seçiniz.")]
        public string Language { get; set; }
        public string PageSlug { get; set; }
        public string GroupContent { get; set; }
        public string SectorGroupLogo { get; set; }
        public int SortOrder { get; set; }
        
    }
}
