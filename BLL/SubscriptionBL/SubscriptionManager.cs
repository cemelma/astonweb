using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myBLOGData;
using System.Data;
using System.Web.Security;
using System.Web;
using DAL.Context;
using DAL.Entities;
using BLL.LogBL;
using log4net;
namespace BLL.SubscriptionBL
{
    public class SubscriptionManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string AddSubscription(string email)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var data = db.Subscription.FirstOrDefault(d => d.Email == email);
                    if (data == null)
                    {
                        Subscription sub = new Subscription();
                        sub.Email = email;
                        sub.CreateTime = DateTime.Now;
                        db.Subscription.Add(sub);
                        db.SaveChanges();
                        return "E-Posta aboneliğiniz gerçekleşmiştir, aboneliğinizin iptali için tekrar mailinizi e-posta aboneliğine girmelisiniz.";
                    }
                    else
                    {
                        db.Subscription.Remove(data);
                        db.SaveChanges();
                        return "E-Posta aboneliğiniz iptal edilmiştir.";                        
                    }
                }
                catch (Exception)
                {
                    return "İşlem sırasında bir hata ile karşılaşıldı,daha sonra deneyiniz.";
                }
            }

        }

        public static List<Subscription> GetSubsciption()
        {
            using (MainContext db = new MainContext())
            {
                return db.Subscription.ToList();
            }
        }


        public static bool DeleteUsers(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.Subscription.FirstOrDefault(d => d.SubscriptionId == id);
                    db.Subscription.Remove(record);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

   
     
    }
}
