using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ProductDetail
    {
        [Key]
        public int DetailId{get;set;}

        public int ProductId { get; set; }

        [Display(Name = "Ürün Kodu")]
        [Required(ErrorMessage = "Ürün Kodunu Giriniz.")]
        public string Code { get; set; }

        [Display(Name = "Malzeme")]
        [Required(ErrorMessage = "Malzeme Adı Giriniz.")]
        public string Material { get; set; }

        [Display(Name = "Birim")]
        [Required(ErrorMessage = "Birim Giriniz.")]
        public string Unit { get; set; }

        [Display(Name = "Ebat")]
        [Required(ErrorMessage = "Ebat Giriniz.")]
        public string Dimension { get; set; }

        [Display(Name = "Ağırlık")]
        [Required(ErrorMessage = "Ağırlık Giriniz.")]
        public string Weight { get; set; }

        [Display(Name = "Araç(Ton)")]
        [Required(ErrorMessage = "Araç(Ton) Giriniz.")]
        public string VehicleTon { get; set; }

        [Display(Name = "B.Rengi ")]
      
        public string ColorWhite { get; set; }

        //public string ColorName { get; set; }

        [Display(Name = "Kırmızı TL/M2")]
      
        public string Price { get; set; }

    }
}
