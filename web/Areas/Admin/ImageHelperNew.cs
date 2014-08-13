using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;

namespace web.Areas.Admin
{
    public class ImageHelperNew
    {
        /// <summary>
        /// Find the right codec
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static ImageCodecInfo GetImageCodec(string extension)
        {
            extension = extension.ToUpperInvariant();
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FilenameExtension.Contains(extension))
                {
                    return codec;
                }
            }
            return codecs[1];
        }

        public static void DestroyImageCashAndSession(int w, int h, int w2 = 0, int h2 = 0)
        {
            if (w!=0 && h!=0)
            {
                HttpContext.Current.Session["_minwidth"] = w.ToString();
                HttpContext.Current.Session["_minheight"] = h.ToString();
                HttpContext.Current.Session["_minwidth2"] = w2;
                HttpContext.Current.Session["_minheight2"] = h2;
                HttpContext.Current.Session.Remove("UploadType");
            }

            if (HttpContext.Current.Session["WorkingImageId"] != null)
            {
                HttpContext.Current.Cache.Remove("ImageKey_" + HttpContext.Current.Session["WorkingImageId"].ToString());
                HttpContext.Current.Session.Remove("WorkingImageId");

                if (HttpContext.Current.Session["ModifiedImageId"] != null)
                {
                    HttpContext.Current.Cache.Remove("ImageKey_" + HttpContext.Current.Session["ModifiedImageId"].ToString());
                    HttpContext.Current.Session.Remove("ModifiedImageId");
                }
            }        
        }
        /// <summary>
        /// Images to byte array.
        /// </summary>
        /// <param name="imageIn">The image in.</param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(System.Drawing.Image image)
        {
            return ImageToByteArray(image, null, null);
        }

        /// <summary>
        /// Images to byte array.
        /// </summary>
        /// <param name="image">The image in.</param>
        /// <param name="extension">The extension.</param>
        /// <param name="encoderParameters">The encoder parameters.</param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(System.Drawing.Image image, string extension, EncoderParameters encoderParameters)
        {
            MemoryStream ms = new MemoryStream();
            if (!string.IsNullOrEmpty(extension) && encoderParameters != null)
                image.Save(ms, GetImageCodec(extension), encoderParameters);
            else
                image.Save(ms, image.RawFormat);
            return ms.ToArray();
        }

        /// <summary>
        /// Bytes the array to image.
        /// </summary>
        /// <param name="byteArrayIn">The byte array in.</param>
        /// <returns></returns>
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}