using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ProductColors
    {
        [Key]
        public int ColorId { get; set; }
        public int ProductId { get; set; }
        public string Adi { get; set; }
        public string RenkKodu { get; set; }
    }
}
