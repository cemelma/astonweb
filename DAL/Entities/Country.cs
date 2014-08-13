using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "İl Adı")]
        [Required(ErrorMessage = "İl ismini giriniz")]
        public string Name { get; set; }
        public int SortNumber { get; set; }
        //public virtual Estate Estate { get; set; }
    }
}
