using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Areas.Admin.Models
{
    public class ProductAddModel
    {
        public Product Product{get;set;}
        public VMProductGroupModel VMProductGroupModel { get; set; }
        public ProductGroup SelectedProductGroup { get; set; }
    }
}