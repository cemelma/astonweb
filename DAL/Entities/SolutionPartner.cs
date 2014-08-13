using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class SolutionPartner
    {
        [Key]
        public int SolutionPartnerId { get; set; }
        [Display(Name = "Çözüm Ortağı Adı")]
        [Required(ErrorMessage = "Çözüm Ortağı İsmini Giriniz.")]
        public string SolutionPartnerName { get; set; }
        public string Logo { get; set; }

        [Display(Name = "Açıklama")]
        public string Content { get; set; }
        public Nullable<DateTime> TimeCreated { get; set; }
        public Nullable<DateTime> TimeUpdated { get; set; }
        public bool Deleted { get; set; }
        public bool Online { get; set; }
        public int SortOrder { get; set; }
        [Url(ErrorMessage = "Url formatı doğru değil.")]
        public string Link { get; set; }

        [Required(ErrorMessage = "Dili Seçiniz.")]
        public string Language { get; set; }
    }
}
