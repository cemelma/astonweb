using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class SocialMedia
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Link Adı")]
        [Required(ErrorMessage = "Adı Giriniz.")]
        public string Name { get; set; }

        [Display(Name = "Logo")]

      

        [Required(ErrorMessage = "Logoyu Giriniz.")]

        public string Logo { get; set; }

        [Display(Name = "Link")]
        [Required(ErrorMessage = "Linki Giriniz.")]

        [Url(ErrorMessage="Uygun Bir Adres Giriniz.")]

        public string LinkName { get; set; }

        public int SortOrder { get; set; }

    }
}
