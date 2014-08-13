using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
namespace BLL.SecurityBL
{
    public class SecurityManager
    {
        public static string Encode(ref string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            return System.Convert.ToBase64String(toEncodeAsBytes);
        }

        public static string Decode(ref string encodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            return System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
        }

        public static string EncodeSHA(string toEncode)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(toEncode, "SHA1");
        }

    }
}
