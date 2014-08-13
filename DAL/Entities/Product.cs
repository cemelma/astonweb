using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Display(Name = "Ürün Grubu")]
        public int ProductGroupId { get; set; }
        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage = "Ürün Adını Giriniz.")]
        public string Name { get; set; }
        [Display(Name = "İçerik")]
       
        public string Content { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public int TopProductGroupId { get; set; }     
        public bool Deleted { get; set; }
        public bool Online { get; set; }
        public DateTime TimeCreated { get; set; }
        public int SortNumber { get; set; }
        [Display(Name = "Ürün Açıklaması")]
        public string PageSlug { get; set; }
        public ProductGroup ProductGroup { get; set; }
        public bool IsShownMain { get; set; }
        //public List<ProductDetail> ProductDetail { get; set; }
        //public List<ProductInformation> ProductInformation { get; set; }
    }
}
