using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class ReferenceWrapperModel
    {
        public References references { get; set; }
        public IList<Photo> photos { get; set; }

        public ReferenceWrapperModel(References references, IList<Photo> photos)
        {
            this.photos = photos;
            this.references = references;
        }
    }
}