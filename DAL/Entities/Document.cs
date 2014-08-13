using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }
        [Required(ErrorMessage="Döküman Grubunu Seçiniz.")]
        public int DocumentGroupId { get; set; }

        [Required(ErrorMessage = "Döküman Adını Giriniz")]
        [Display(Name = "Döküman Adı")]
        public string Name { get; set; }

       
        [Display(Name = "Döküman")]
        public string DocumentFile { get; set; }

        public bool Online { get; set; }
        public bool Deleted { get; set; }

        public Nullable<DateTime> TimeCreated { get; set; }
        public Nullable<DateTime> TimeUpdated { get; set; }
        [Required(ErrorMessage = "Dili Seçiniz.")]
        public string Language { get; set; }
        public int SortNumber { get; set; }
    }
}
