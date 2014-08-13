using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Languages
    {
        [Key]
        public int LanguageId { get; set; }
        public string Language { get; set; }
        public string Culture { get; set; }

    }
}
