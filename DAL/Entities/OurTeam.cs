using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class OurTeam
    {
        [Key]
        public int OurTeamId { get; set; }

        [Required(ErrorMessage = "Bu alan gereklidir")]
        [Display(Name="Kişi Adı")]
        public string Name { get; set; }

        [Display(Name = "İçerik")]
        [Required(ErrorMessage = "Bu alan gereklidir")]
        public string Content { get; set; }

        [Display(Name = "Linkedin Adres")]
        public string Linkedin { get; set; }

        public bool Online { get; set; }
        public bool Deleted { get; set; }

        [Display(Name = "Resim")]
        public string Image { get; set; }

        [Display(Name = "Eklenme Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<DateTime> TimeCreated { get; set; }
        public Nullable<DateTime> TimeUpdated { get; set; }

        [Required(ErrorMessage = "Dili Seçiniz.")]
        public string Language { get; set; }
        public string PageSlug { get; set; }
        public int SortOrder { get; set; }
    }
}
