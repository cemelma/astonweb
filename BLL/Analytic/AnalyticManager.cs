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
namespace BLL.AccountBL
{
    public class AnalyticManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
     

        //public static bool AddAnalytic(Analytic record)
        //{
        //    using (MainContext db = new MainContext())
        //    {
        //        try
        //        {
        //            Analytic anl = GetAnalytic();
        //            if (anl == null)
        //            {
        //                db.Analytic.Add(record);
        //                db.SaveChanges();
        //                return true;
        //            }
        //            else
        //            {
        //                //record.Code = record.Code;
        //                db.SaveChanges();
        //                return true;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return false;
        //        }
        //    }

        //}

        public static bool AddAnalytic(string code)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Analytic anl = db.Analytic.FirstOrDefault();
                    if (anl == null)
                    {
                        anl = new Analytic();
                        anl.Code = code;
                        db.Analytic.Add(anl);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        anl.Code = code;
                        db.SaveChanges();
                        db.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public static Analytic GetAnalytic()
        {
            using (MainContext db = new MainContext())
            {
                Analytic list = db.Analytic.FirstOrDefault();
                return list;
            }
        }

   
     
    }
}
