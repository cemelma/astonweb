using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class References
    {
        [Key]
        public int ReferenceId { get; set; }
        [Display(Name="Referans Adı")]
        [Required(ErrorMessage="Referans İsmini Giriniz.")]
        public string ReferenceName { get; set; }
        public string Logo { get; set; }

        [Display(Name = "Açıklama")]
        public string Content { get; set; }
        public Nullable<DateTime> TimeCreated { get; set; }
        public Nullable<DateTime> TimeUpdated { get; set; }
        public bool Deleted { get; set; }
        public bool Online { get; set; }
        public int  SortOrder { get; set; }
        [Url(ErrorMessage = "Url formatı doğru değil.")]
        public string Link{get;set;}

        [DisplayName("Galeri")]
        public string GalleryId { get; set; }

      
        public string Language { get; set; }
    }
}
