using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Web.Helpers;

namespace web.Areas.Admin.Helpers
{
    public class ImageHelper
    {
        int width=0;
        int height=0;
        public ImageHelper()
        {
        }

        public ImageHelper(int width, int height)
        {
            this.width=width;
            this.height=height;
        }

        public string SaveThumbnail(HttpPostedFileBase httpPostedFileBase, string path,string fileName)
        {
            
            string dir = GetDirectory(path);
            Image img = null;
            try
            {
                img = Image.FromStream(httpPostedFileBase.InputStream);
                //fileName = fileName + Path.GetExtension(httpPostedFileBase.FileName);
                Resize(dir, width,height, fileName, img, img.RawFormat, String.Empty, true);
                return HttpUtility.UrlPathEncode(Path.Combine(path, fileName));
            }
            finally
            {
                if (img != null)
                    img.Dispose();
                httpPostedFileBase.InputStream.Dispose();
            }
        }

        
        private static void Delete(string path)
        {
            File.Delete(HttpContext.Current.Server.MapPath(path));
        }

        private static string GetDirectory(string dir)
        {
            dir = HttpContext.Current.Server.MapPath(dir);
            TryCreate(dir);
            return dir;
        }

        private static void TryCreate(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }


        public  string ResizeFromStream(string path, string fileName,
             Image img)
        {
            string resized = fileName;
            //Image img = Image.FromStream(Buffer);

            //Determine image format 
            ImageFormat imgFormat = img.RawFormat;



            resized = Resize(path, 700, 2048, resized, img, imgFormat, "r_", false);
            //thumbnail = Resize(path, 64, 64, thumbnail, img, imgFormat, "thumb_r_");

            //release used resources 
            //img.Dispose();
            //Buffer.Close();
            return resized;
        }

        private static string Resize(string path, int maxWidth, int maxHeight, string resized,
            Image img, ImageFormat imgFormat, string prefix, bool save)
        {
            //get image original width and height 
            int oldWidth = img.Width;
            int oldHeight = img.Height;
            int newWidth = oldWidth;
            int newHeight = oldHeight;

            if (oldWidth > maxWidth)
            {
                //set new width and height 
                double factor = maxWidth / (double)oldWidth;
                newWidth = Convert.ToInt32(factor * oldWidth);
                newHeight = Convert.ToInt32(factor * oldHeight);
            }
            if (newHeight > maxHeight)
            {
                double factor = maxHeight / (double)newHeight;
                newWidth = Convert.ToInt32(factor * newWidth);
                newHeight = Convert.ToInt32(factor * newHeight);
            }

            if (oldWidth > newWidth || oldHeight > newHeight)
            {
                //create new bitmap 
                Bitmap bmpResized = new Bitmap(img, newWidth, newHeight);
                try
                {
                    resized = String.Concat(prefix, resized);
                    //save bitmap to disk 
                    bmpResized.Save(Path.Combine(path, resized), imgFormat);
                }
                finally
                {
                    bmpResized.Dispose();
                }
            }
            else if (save)
            {
                img.Save(Path.Combine(path, resized), imgFormat);
            }
            return resized;
        }


        public static bool WaterMark(string url,int fontsize)
        {
            try
            {
                string wpath = HttpContext.Current.Server.MapPath(url);
                WebImage supermanImage = new WebImage(wpath);
                //supermanImage.AddTextWatermark("Zeynel Yayla ©", "White", fontsize, "Regular", "Consolas", "Right", "Bottom", 70, 5);
                string watermarkImageFilePath = HttpContext.Current.Server.MapPath("/Content/Images/fligran/Fligran.jpg");
                supermanImage.AddImageWatermark(watermarkImageFilePath, 110, 61, "Right", "Bottom", 70, 5);

                if (System.IO.File.Exists(wpath))
                {
                    System.IO.File.Delete(wpath);
                }

                supermanImage.Save(wpath,"jpg");
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        public static bool WaterMarkThumb(string url)
        {
            try
            {
                string wpath = HttpContext.Current.Server.MapPath(url);
                WebImage supermanImage = new WebImage(wpath);
                //supermanImage.AddTextWatermark("Zeynel Yayla ©", "White", fontsize, "Regular", "Consolas", "Right", "Bottom", 70, 5);
                string watermarkImageFilePath = HttpContext.Current.Server.MapPath("/Content/Images/fligran/FligranThumb.jpg");                
                supermanImage.AddImageWatermark(watermarkImageFilePath, 40, 22, "Right", "Bottom", 70, 5);

                if (System.IO.File.Exists(wpath))
                {
                    System.IO.File.Delete(wpath);
                }

                supermanImage.Save(wpath, "jpg");
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}