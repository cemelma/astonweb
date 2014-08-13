using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class ProductPriceModel
    {
        public List<ProductPriceDetail> Info = new List<ProductPriceDetail>();
        
        //public List<Product> products { get; set; }
        //public List<ProductInformation> productsinfo { get; set; }
        //public List<ProductHeaders> headers { get; set; }

    }


    public class ProductPriceDetail
    {
        public string ProductName { get; set; }
        public string PageSlug { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public List<ProductInformation> productsinfo { get; set; }
        public ProductHeaders headers { get; set; }

    }
}