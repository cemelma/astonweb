using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Town
    {
        [Key]
        public int Id { get; set; }
        public int CountryId { get; set; }
        [Display(Name="İlçe Adı")]
        [Required(ErrorMessage="Şehir ismini giriniz")]
        public string Name { get; set; }
    }
}
