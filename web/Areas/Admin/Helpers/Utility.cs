using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;


namespace web.Areas.Admin.Helpers
{
    public class Utility
    {
       

        public static string HtmlParse(string html)
        {
            string newword=Regex.Replace(html, @"<(.|\n)*?>", string.Empty);
            newword = newword.Replace("&nbsp;", " ");
            newword = newword.Replace("&uuml;", "ü");
            newword = newword.Replace("&Uuml;", "Ü");
            newword = newword.Replace("&#39;", "\'");
            newword = newword.Replace("&ouml;", "ö");
            newword = newword.Replace("&Ouml;", "Ö");
            newword = newword.Replace("&ccedil;", "ç");
            newword = newword.Replace("&Ccedil;", "Ç");
            newword=newword.Replace("&quot;", "\"");
            newword = newword.Replace("&hellip;", "...");
            newword = newword.Replace("&lsquo;", "\'");
            newword = newword.Replace("&rsquo;", "\'");
            newword = newword.Replace("&ldquo;", "\"");
            newword = newword.Replace("&rdquo;", "\"");

            newword = newword.TrimStart();
            newword = newword.TrimEnd();
            newword = Regex.Replace(newword, @"\s", " ");

            return newword;
        }

        public static string SetPagePlug(string word)
        {
            string returnvalue = Regex.Replace(word, @"\s+", " ");
            returnvalue = returnvalue.Replace("ü", "u");
            returnvalue = returnvalue.Replace("ğ", "g");
            returnvalue = returnvalue.Replace("ö", "o");
            returnvalue = returnvalue.Replace("ş", "s");
            returnvalue = returnvalue.Replace("ç", "c");
            returnvalue = returnvalue.Replace("ı", "i");
            returnvalue = returnvalue.Replace(" ", "-");
            returnvalue = returnvalue.Replace("\'", "");
            returnvalue = returnvalue.Replace("İ", "i");
            returnvalue = returnvalue.Replace("I", "i");
            returnvalue = returnvalue.Replace("Ğ", "g");
            returnvalue = returnvalue.Replace("G", "g");
            returnvalue = returnvalue.Replace("Ş", "s");
            returnvalue = returnvalue.Replace("Ç", "c");
            returnvalue = returnvalue.Replace("Ü", "u");
            returnvalue = returnvalue.Replace("Ö", "o");
            returnvalue = returnvalue.Replace("/", "-");
            returnvalue = returnvalue.Replace("?", "");
            returnvalue = returnvalue.Replace("\"", "");
            returnvalue = returnvalue.Replace("*", "");
            returnvalue = returnvalue.Replace("'", "");
            returnvalue = returnvalue.Replace("\"", "");
            returnvalue = returnvalue.Replace("+", "");
            returnvalue = returnvalue.Replace(",", "");
            returnvalue = returnvalue.Replace(";", "");
            returnvalue = returnvalue.Replace(":", "");
            returnvalue = returnvalue.Replace("!", "");
            returnvalue = returnvalue.Replace("...", "");
            returnvalue = returnvalue.Replace(".", "");
            returnvalue = returnvalue.Replace("&", "-");
            return returnvalue.ToLower();
        }

        public static DateTime ControlDateTime(string dateval)
        {
            DateTime correctval=DateTime.Now;
            bool isdate=DateTime.TryParse(dateval,out correctval);
            if(isdate)
                return correctval;
            else
            {
                correctval=DateTime.Now;
                return correctval;
            }
        }

        public static double[] RateImage(Bitmap img, int donusturme)
        {
            double[] vals = new double[2];
            double yukseklik = img.Height;
            double genislik = img.Width;
            double oran = 0;

            if (genislik >= donusturme)
            {
                oran = genislik / yukseklik;
                genislik = donusturme;
                yukseklik = genislik / oran;
            }
            vals[0] = genislik;
            vals[1] = yukseklik;
            return vals;
        }
    }
}