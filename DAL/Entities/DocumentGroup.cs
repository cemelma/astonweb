using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DocumentGroup
    {
        [Key]
        public int DocumentGroupId { get; set; }

        [Required(ErrorMessage = "Grup Adını Giriniz")]
        [Display(Name = "Grup Adı")]
        public string GroupName { get; set; }

        public bool Online { get; set; }
        public bool Deleted { get; set; }
       
        public Nullable<DateTime> TimeCreated { get; set; }
        public Nullable<DateTime> TimeUpdated { get; set; }

        [Required(ErrorMessage = "Dili Seçiniz.")]
        public string Language { get; set; }
        public string PageSlug { get; set; }
        public int SortNumber { get; set; }
    }
}
