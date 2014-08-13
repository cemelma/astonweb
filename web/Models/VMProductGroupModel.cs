using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class VMProductGroupModel
    {
        public List<ProductGroup> ProductGroup { get; set; }
        public List<Product> Products { get; set; }
        public ProductGroup SelectedProductGroup { get; set; }
    }
}