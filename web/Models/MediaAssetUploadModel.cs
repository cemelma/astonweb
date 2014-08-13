using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class MediaAssetUploadModel
    {
        public HttpPostedFileBase fileData { get; set; }
        public string SecurityToken { get; set; }
        public string Filename { get; set; }
    }
}