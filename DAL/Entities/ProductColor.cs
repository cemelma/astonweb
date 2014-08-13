using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ProductColor
    {
        [Key]
        public int ProductColorId { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "Renk Adı")]
        [Required(ErrorMessage = "Ürün Kodunu Giriniz.")]
        public string Code { get; set; }

        [Display(Name = "Renk")]
        [Required(ErrorMessage = "Ürün Kodunu Giriniz.")]
        public string Resim { get; set; }
    }
}
