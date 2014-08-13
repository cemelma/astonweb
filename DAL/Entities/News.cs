using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }

        [Required(ErrorMessage = "Haber Başlığını Giriniz")]
        [Display(Name="Başlık")]
        public string Header { get; set; }

        [Display(Name = "Haber Spotu")]
        public string Spot { get; set; }

        [Display(Name = "İçerik")]
        public string Content { get; set; }

        [Display(Name = "Ana Sayfada Gösterim")]
        public string IsHomePage { get; set; }

        public bool Online { get; set; }
        public bool Deleted { get; set; }
        public int TypeId { get; set; }

        [Display(Name="Eklenme Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<DateTime> TimeCreated { get; set; }
        public Nullable<DateTime> TimeUpdated { get; set; }
        
        [Display(Name = "Haber Resmi")]
        public string NewsImage { get; set; }
       
        public string Language { get; set; }
        public string PageSlug { get; set; }
        [Display(Name = "Anasayfada Göster")]
        public bool ShowInMenu { get; set; }
        public int SortOrder { get; set; }
    }
}
