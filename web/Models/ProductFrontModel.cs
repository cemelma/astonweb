using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class ProductFrontModel
    {
        public Product product { get; set; }
        public ProductHeaders headers { get; set; }
        public List<ProductInformation> ProductInfo { get; set; }
        public List<ProductColors> Colors { get; set; }
    }
}