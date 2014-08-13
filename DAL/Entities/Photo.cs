using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }

        public int CategoryId { get; set; }
        public int ItemId { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public DateTime TimeCreated { get; set; }
        public bool Online { get; set; }
        public int SortOrder { get; set; }
        public string Thumbnail { get; set; }
        public string Link { get; set; }
    }
}
