using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ProdCategoryBL
{
    public class ProdCategoryManager
    {
        public static bool Add(ProdCategory record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    record.Online = true;
                    record.SortNumber = 9999;
                    db.ProdCategory.Add(record);
                    db.SaveChanges();

                    //LogtrackManager logkeeper = new LogtrackManager();
                    //logkeeper.LogDate = DateTime.Now;
                    //logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                    //logkeeper.Message = LogMessages.ProductGroupAdded;
                    //logkeeper.User = HttpContext.Current.User.Identity.Name;
                    //logkeeper.Data = record.GroupName;
                    //logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }


        public static string GetMainCategories(string lang)
        {
            return "";
            //using (MainContext db = new MainContext())
            //{
            //    try {
            //        return db.ProdCategory.Select(
            //            x => new
            //            {
            //                ProdCategoryId = x.ProdCategoryId,
            //                Name = x.Name
            //            }
            //            ).ToJSON();
            //    }
            //    catch(Exception ex) { 
            //        return null;
            //    }
            //}
        }
    }

   
}
