using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class HumanResource
    {
        public int Id { get; set; }
       
        [Display(Name="Başlık")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Pozisyon Giriniz")]
        [Display(Name = "Pozisyon")]
        public string PositionName { get; set; }

        [Required(ErrorMessage = "İçerik Giriniz")]
        [Display(Name = "İçerik")]
        public string Content { get; set; }

        public bool Online { get; set; }
        public Nullable<DateTime> TimeCreated { get; set; }
        [Display(Name = "Menü Adı")]
        public string MenuName { get; set; }

        public int SortOrder{get;set;}
    }
}
