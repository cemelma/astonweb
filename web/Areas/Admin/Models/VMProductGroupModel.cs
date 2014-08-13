using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web.Areas.Admin.Models
{
    public class VMProductGroupModel
    {
        public IEnumerable<ProductGroup> ProductGroup { get; set; }
        public List<Product> Products { get; set; }
        public ProductGroup SelectedProductGroup { get; set; }
    }
}