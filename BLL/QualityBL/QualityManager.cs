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
namespace BLL.QualityBL
{
    public class QualityManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<Quality> GetQualityList(string language,bool ismachine)
        {
            using (MainContext db = new MainContext())
            {
                var Quality_list = db.Quality.Where(d => d.Deleted == false && d.Language == language && d.IsMachine == ismachine).OrderByDescending(d => d.TimeCreated).OrderBy(d => d.SortOrder).ToList();
                return Quality_list;
            }
        }

        public static List<Quality> GetQualityList(string language, int TypeId)
        {
            using (MainContext db = new MainContext())
            {
                var Quality_list = db.Quality.Where(d => d.Deleted == false && d.Language == language && d.TypeId == TypeId).OrderByDescending(d => d.TimeCreated).OrderBy(d => d.SortOrder).ToList();
                return Quality_list;
            }
        }

        public static List<Quality> GetQualityListForFront(string language, bool ismachine)
        {
            using (MainContext db = new MainContext())
            {
                var Quality_list = db.Quality.Where(d => d.Deleted == false && d.Language == language && d.Online == true && d.ShowInMenu==true && d.IsMachine==ismachine).OrderByDescending(d => d.TimeCreated).OrderBy(d => d.SortOrder).ToList();
                return Quality_list;
            }
        }

        public static List<Quality> GetQualityListForFrontMachine(string language, bool ismachine)
        {
            using (MainContext db = new MainContext())
            {
                var Quality_list = db.Quality.Where(d => d.Deleted == false && d.Language == language && d.Online == true && d.IsMachine == ismachine).OrderByDescending(d => d.TimeCreated).OrderBy(d => d.SortOrder).ToList();
                return Quality_list;
            }
        }

        public static Quality GetQualityItem(int id)
        {
            using (MainContext db = new MainContext())
            {
                Quality Quality = db.Quality.Where(d => d.QualityId == id).SingleOrDefault();
                return Quality;
            }
        }

        public static bool AddQuality(Quality record)
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
                    db.Quality.Add(record);
                    db.SaveChanges();
                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Haber.ToString();
                    logkeeper.Message = LogMessages.QualityAdded;
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
                var list = db.Quality.SingleOrDefault(d => d.QualityId == id);
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
                    var record = db.Quality.FirstOrDefault(d => d.QualityId == id);
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
                    logkeeper.Message = LogMessages.QualityDeleted;
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

        public static Quality GetQualityById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Quality record = db.Quality.Where(d => d.QualityId == nid).SingleOrDefault();
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

        public static dynamic EditQuality(Quality Qualitymodel)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Quality record = db.Quality.Where(d => d.QualityId == Qualitymodel.QualityId && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        record.Header = Qualitymodel.Header;
                        record.Language = Qualitymodel.Language;
                        record.Content = Qualitymodel.Content;
                        record.ShowInMenu = Qualitymodel.ShowInMenu;
                        record.TypeId = Qualitymodel.TypeId;
                        if (!string.IsNullOrEmpty(Qualitymodel.NewsImage) && Qualitymodel.NewsImage != "/Content/images/front/noimage.jpeg")
                        {
                            string filePath = HttpContext.Current.Server.MapPath(record.NewsImage);
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                            record.NewsImage = Qualitymodel.NewsImage;
                        }
                        record.PageSlug = Qualitymodel.PageSlug;
                        record.TimeUpdated = DateTime.Now;
                        record.Spot = Qualitymodel.Spot;

                        db.SaveChanges();

                        LogtrackManager logkeeper = new LogtrackManager();
                        logkeeper.LogDate = DateTime.Now;
                        logkeeper.LogProcess = EnumLogType.Haber.ToString();
                        logkeeper.Message = LogMessages.QualityEdited;
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
                        Quality sortingrecord = db.Quality.SingleOrDefault(d => d.QualityId == mid);
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
