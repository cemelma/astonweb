using BLL.LogBL;
using DAL.Context;
using DAL.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.InstituionalBL
{
    public class PageManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static bool Create(Page record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.TimeUpdated = DateTime.Now;
                    record.Deleted = false;
                    record.SortOrder = 9999;
                    if (record.Content == null) record.Content = "İçerik Yok";
                    db.Page.Add(record);
                    db.SaveChanges();
                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = "Hakkımızda Ek Sayfa";
                    logkeeper.Message = "Sayfa Eklendi";
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.Header;
                    logkeeper.AddInfoLog(logger);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static bool Edit(Page record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Page page = db.Page.SingleOrDefault(d => d.PageId == record.PageId);
                    page.Language = record.Language;
                    page.Header = record.Header;
                    page.PageSlug = record.PageSlug;
                    if (record.Content == null) record.Content = "İçerik Yok";
                    page.Content = record.Content;
                    page.TimeUpdated = DateTime.Now;
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = "Hakkımızda Ek Sayfa";
                    logkeeper.Message = "Sayfa Düzenleme";
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.Header;
                    logkeeper.AddInfoLog(logger);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static List<Page> List(string lang)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    List<Page> list = db.Page.Where(d => d.Language == lang && d.Deleted == false).OrderBy(d=>d.SortOrder).ToList();
                    return list;
                }
                catch (Exception)
                {
                    return new List<Page>();
                }
            }
        }

        public static bool Sort(string[] idsList)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    int row = 0;
                    foreach (string id in idsList)
                    {
                        int mid = Convert.ToInt32(id);
                        Page record = db.Page.SingleOrDefault(d => d.PageId == mid);
                        record.SortOrder = Convert.ToInt32(row);
                        db.SaveChanges();
                        row++;
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static Page Get(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Page rec = db.Page.FirstOrDefault(d => d.PageId == id);
                    return rec;
                }
                catch (Exception)
                {
                    return new Page();
                }
            }
        }

        public static bool Delete(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.Page.FirstOrDefault(d => d.PageId == id);
                    record.Deleted = true;

                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = "Hakkımızda Ek Sayfa";
                    logkeeper.Message = "Sayfa Silindi";
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.Header;
                    logkeeper.AddInfoLog(logger);

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
