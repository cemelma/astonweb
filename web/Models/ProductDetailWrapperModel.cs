using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class ProductDetailWrapperModel
    {
        public Product product { get; set; }
        public IEnumerable<ProductGroup> productgroups { get; set; }
        public IEnumerable<Photo> photos { get; set; }

        public ProductDetailWrapperModel(Product product, IEnumerable<ProductGroup> productgroups, IEnumerable<Photo> photos)
        {
            this.product = product;
            this.productgroups = productgroups;
            this.photos = photos;
        }
    }
}