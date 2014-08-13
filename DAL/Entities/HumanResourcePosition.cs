using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class HumanResourcePosition
    {
        [Key]
        public int HumanResourcePositionId { get; set; }
        [Display(Name="Pozisyon Adı")]
        [Required(ErrorMessage="Pozisyon İsmini Giriniz.")]
        public string HumanResourcePositionName { get; set; }
       
        [Display(Name = "Açıklama")]
        public string Content { get; set; }

        [Display(Name = "İş Tanımı")]
        public string Workdef { get; set; }
        
        public Nullable<DateTime> TimeCreated { get; set; }
        public Nullable<DateTime> TimeUpdated { get; set; }
        public bool Deleted { get; set; }
        public bool Online { get; set; }
        public int  SortOrder { get; set; }
        
        //[Required(ErrorMessage = "Dili Seçiniz.")]
        //[Display(Name = "Dil")]
        public string Language { get; set; }
    }
}
