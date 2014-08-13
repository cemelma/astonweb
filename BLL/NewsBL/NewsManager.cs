using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BLL.LogBL;
using DAL.Context;
using DAL.Entities;
using log4net;
namespace BLL.NewsBL
{
    public class NewsManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<News> GetNewsList(string language)
        {
            using (MainContext db = new MainContext())
            {
                var news_list = db.News.Where(d => d.Deleted == false && d.Language == language).OrderByDescending(d => d.TimeCreated).OrderBy(d => d.SortOrder).ToList();
                return news_list;
            }
        }

        public static List<News> GetNewsList(string language, int TypeId)
        {
            using (MainContext db = new MainContext())
            {
                var news_list = db.News.Where(d => d.Deleted == false && d.Language == language && d.TypeId == TypeId).OrderByDescending(d => d.TimeCreated).OrderBy(d => d.SortOrder).ToList();
                return news_list;
            }
        }

        public static List<News> GetNewsListForFront(string language)
        {
            using (MainContext db = new MainContext())
            {
                var news_list = db.News.Where(d => d.Deleted == false && d.Language == language && d.Online == true && d.ShowInMenu==true).OrderByDescending(d => d.TimeCreated).OrderBy(d => d.SortOrder).ToList();
                return news_list;
            }
        }

        public static News GetNewsItem(int id)
        {
            using (MainContext db = new MainContext())
            {
                News news = db.News.Where(d => d.NewsId == id).SingleOrDefault();
                return news;
            }
        }

        public static bool AddNews(News record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    if (!record.TimeCreated.HasValue)
                        record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    record.Online = true;
                    record.SortOrder = 9999;
                    db.News.Add(record);
                    db.SaveChanges();
                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Haber.ToString();
                    logkeeper.Message = LogMessages.NewsAdded;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.Header;
                    logkeeper.AddInfoLog(logger);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
           
        }

        public static bool UpdateStatus(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.News.SingleOrDefault(d => d.NewsId == id);
                try
                {

                    if (list != null)
                    {
                        list.Online = list.Online == true ? false : true;
                        db.SaveChanges();

                    }
                    return list.Online;

                }
                catch (Exception)
                {
                    return list.Online;
                }
            }
        }

        public static bool Delete(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.News.FirstOrDefault(d => d.NewsId == id);
                    record.Deleted = true;
                    if (record.NewsImage != "/Content/images/front/noimage.jpeg")
                    {

                        string filePatht = HttpContext.Current.Server.MapPath(record.NewsImage);
                        if (System.IO.File.Exists(filePatht))
                        {
                            System.IO.File.Delete(filePatht);
                        }
                    }

                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Haber.ToString();
                    logkeeper.Message = LogMessages.NewsDeleted;
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

        public static News GetNewsById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    News record = db.News.Where(d => d.NewsId == nid).SingleOrDefault();
                    if (record != null)
                        return record;
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }


        }

        public static dynamic EditNews(News newsmodel)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    News record = db.News.Where(d => d.NewsId == newsmodel.NewsId && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        record.Header = newsmodel.Header;
                        record.Language = newsmodel.Language;
                        record.Content = newsmodel.Content;
                        record.ShowInMenu = newsmodel.ShowInMenu;
                        record.TypeId = newsmodel.TypeId;
                        if (!string.IsNullOrEmpty(newsmodel.NewsImage) && newsmodel.NewsImage != "/Content/images/front/noimage.jpeg")
                        {
                            string filePath = HttpContext.Current.Server.MapPath(record.NewsImage);
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                            record.NewsImage = newsmodel.NewsImage;
                        }
                        record.PageSlug = newsmodel.PageSlug;
                        record.TimeUpdated = DateTime.Now;
                        record.Spot = newsmodel.Spot;

                        db.SaveChanges();

                        LogtrackManager logkeeper = new LogtrackManager();
                        logkeeper.LogDate = DateTime.Now;
                        logkeeper.LogProcess = EnumLogType.Haber.ToString();
                        logkeeper.Message = LogMessages.NewsEdited;
                        logkeeper.User = HttpContext.Current.User.Identity.Name;
                        logkeeper.Data = record.Header;
                        logkeeper.AddInfoLog(logger);
                        return true;
                    }
                    else
                        return false;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool SortRecords(string[] idsList)
        {
            using (MainContext db = new MainContext())
            {
                try
                {

                    int row = 0;
                    foreach (string id in idsList)
                    {
                        int mid = Convert.ToInt32(id);
                        News sortingrecord = db.News.SingleOrDefault(d => d.NewsId == mid);
                        sortingrecord.SortOrder = Convert.ToInt32(row);
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
    }
}
