using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class HumanResourceCv
    {
        [Key]
        public int HumanResourceCvId { get; set; }

        public int HumanResourcePositionId { get; set; }

        [Display(Name="Dosya")]
        public string FileUrl { get; set; }

        public bool Deleted { get; set; }

        [Display(Name = "Gönderim Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<DateTime> TimeCreated { get; set; }

    }
}
