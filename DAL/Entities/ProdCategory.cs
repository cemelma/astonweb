using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class ProdCategory
    {
        [Key]
        public int ProdCategoryId { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; }

        public string Lang { get; set; }

        public DateTime TimeCreated { get; set; }

        public bool Deleted { get; set; }

        public bool Online { get; set; }

        public int SortNumber { get; set; }
    }
}