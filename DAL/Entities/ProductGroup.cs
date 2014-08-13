using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class ProductGroup
    {
        [Key]
        public int ProductGroupId { get; set; }

        [Required(ErrorMessage = "Grup Adını Giriniz")]
        [Display(Name = "Grup Adı")]
        public string GroupName { get; set; }

        [Display(Name = "Grup Adı")]
        public int TopProductId { get; set; }

        public string GroupImage { get; set; }

        public bool Online { get; set; }
        public bool Deleted { get; set; }

        public List<Product> Product { get; set; }

        public Nullable<DateTime> TimeCreated { get; set; }
        public Nullable<DateTime> TimeUpdated { get; set; }

        [Required(ErrorMessage = "Dili Seçiniz.")]
        public string Language { get; set; }
        public string PageSlug { get; set; }
        public int SortNumber { get; set; }

        public static object GetProductGroupList()
        {
            throw new NotImplementedException();
        }
    }
}
