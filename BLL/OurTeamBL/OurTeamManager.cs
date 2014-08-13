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
namespace BLL.OurTeamBL
{
    public class OurTeamManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<OurTeam> GetOurTeamList(string language)
        {
            using (MainContext db = new MainContext())
            {
                var OurTeam_list = db.OurTeam.Where(d => d.Deleted == false && d.Language == language).OrderByDescending(d => d.TimeCreated).OrderBy(d => d.SortOrder).ToList();
                return OurTeam_list;
            }
        }

        public static List<OurTeam> GetOurTeamListForFront(string language)
        {
            using (MainContext db = new MainContext())
            {
                var OurTeam_list = db.OurTeam.Where(d => d.Deleted == false && d.Language == language && d.Online == true).OrderByDescending(d => d.TimeCreated).OrderBy(d => d.SortOrder).ToList();
                return OurTeam_list;
            }
        }

        public static OurTeam GetOurTeamItem(int id)
        {
            using (MainContext db = new MainContext())
            {
                OurTeam OurTeam = db.OurTeam.Where(d => d.OurTeamId == id).SingleOrDefault();
                return OurTeam;
            }
        }

        public static bool AddOurTeam(OurTeam record)
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
                    db.OurTeam.Add(record);
                    db.SaveChanges();
                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Haber.ToString();
                    logkeeper.Message = "Ekibimiz kaydı eklendi";
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.Name;
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
                var list = db.OurTeam.SingleOrDefault(d => d.OurTeamId == id);
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
                    var record = db.OurTeam.FirstOrDefault(d => d.OurTeamId == id);
                    record.Deleted = true;

                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Haber.ToString();
                    logkeeper.Message = "Ekibimiz Kaydı Silindi";
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.Name;
                    logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static OurTeam GetOurTeamById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    OurTeam record = db.OurTeam.Where(d => d.OurTeamId == nid).SingleOrDefault();
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

        public static dynamic EditOurTeam(OurTeam OurTeammodel)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    OurTeam record = db.OurTeam.Where(d => d.OurTeamId == OurTeammodel.OurTeamId && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        record.Name = OurTeammodel.Name;
                        record.Language = OurTeammodel.Language;
                        record.Content = OurTeammodel.Content;
                        if (!string.IsNullOrEmpty(OurTeammodel.Image))
                        {
                            record.Image = OurTeammodel.Image;
                        }
                        record.PageSlug = OurTeammodel.PageSlug;
                        record.TimeUpdated = DateTime.Now;
                        record.Linkedin = OurTeammodel.Linkedin;

                        db.SaveChanges();

                        LogtrackManager logkeeper = new LogtrackManager();
                        logkeeper.LogDate = DateTime.Now;
                        logkeeper.LogProcess = EnumLogType.Haber.ToString();
                        logkeeper.Message = "Ekibimiz Kaydı Güncellendi";
                        logkeeper.User = HttpContext.Current.User.Identity.Name;
                        logkeeper.Data = record.Name;
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
                        OurTeam sortingrecord = db.OurTeam.SingleOrDefault(d => d.OurTeamId == mid);
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
