using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using BLL.LogBL;
using System.Web;
using log4net;

namespace BLL.HRBL
{
    public class HumanResourceManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static HumanResource GetHRByLanguage(string language)
        {
            using (MainContext db = new MainContext())
            {
                HumanResource instional_info = db.HumanResource.SingleOrDefault();
                return instional_info;
            }
        }

        public static List<HumanResource> GetHRList()
        {
            using (MainContext db = new MainContext())
            {
                List<HumanResource> instional_info = db.HumanResource.ToList();
                return instional_info;
            }
        }


        public static bool EditHumanResource(HumanResource record)
        {
            using (MainContext db = new MainContext())
            {
                HumanResource editrecord = db.HumanResource.SingleOrDefault();

                if (editrecord == null)
                {
                    editrecord = new HumanResource();
                    editrecord.Content = record.Content;
                    db.HumanResource.Add(editrecord);
                }
                else
                {
                    editrecord.Content = record.Content;
                }

                db.SaveChanges();

                return true;
            }
        }

        public static List<HumanResourcePosition> GetHumanResourcePositionList(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.HumanResourcePosition.Where(d => d.Deleted == false && d.Language == language).OrderBy(d => d.SortOrder).ToList();
                return list;
            }
        }


        public static List<HumanResource> GetHumanResourcePositionList()
        {
            using (MainContext db = new MainContext())
            {
                var list = db.HumanResource.Where(d => d.Online == true).OrderBy(d => d.SortOrder).ToList();
                return list;
            }
        }

        public static List<HumanResourcePosition> GetHumanResourcePositionListForFront(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.HumanResourcePosition.Where(d => d.Deleted == false && d.Language == language && d.Online == true).OrderBy(d => d.SortOrder).ToList();
                return list;
            }
        }

        public static bool AddHumanResourcePosition(HumanResource record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    if (!record.TimeCreated.HasValue)
                        record.TimeCreated = DateTime.Now;

                    record.Online = true;
                    db.HumanResource.Add(record);
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.InsanKaynaklari.ToString();
                    logkeeper.Message = LogMessages.HumanResourcePositionAdded;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.PositionName;
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
                var list = db.HumanResource.SingleOrDefault(d => d.Id == id);
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
                    var record = db.HumanResource.FirstOrDefault(d => d.Id == id);
                    db.HumanResource.Remove(record);

                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.InsanKaynaklari.ToString();
                    logkeeper.Message = LogMessages.HumanResourcePositionDeleted;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.PositionName;
                    logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static HumanResource GetHumanResourcePositionById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    HumanResource record = db.HumanResource.Where(d => d.Id == nid).SingleOrDefault();
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

        public static bool EditHumanResourcePosition(HumanResource HumanResourcePositionmodel)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    HumanResource record = db.HumanResource.Where(d => d.Id == HumanResourcePositionmodel.Id).SingleOrDefault();
                    if (record != null)
                    {
                        record.Content = HumanResourcePositionmodel.Content;
                        record.PositionName = HumanResourcePositionmodel.PositionName;



                        db.SaveChanges();

                        LogtrackManager logkeeper = new LogtrackManager();
                        logkeeper.LogDate = DateTime.Now;
                        logkeeper.LogProcess = EnumLogType.InsanKaynaklari.ToString();
                        logkeeper.Message = LogMessages.HumanResourcePositionEdited;
                        logkeeper.User = HttpContext.Current.User.Identity.Name;
                        logkeeper.Data = record.PositionName;
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
                        HumanResource sortingrecord = db.HumanResource.SingleOrDefault(d => d.Id == mid);
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

        public static bool AddHumanResourceCv(HumanResourceCv record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    db.HumanResourceCv.Add(record);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static List<HumanResourceCv> GetHumanResourceCV()
        {
            using (MainContext db = new MainContext())
            {
                var list = db.HumanResourceCv.Where(d => d.Deleted == false).OrderByDescending(d => d.TimeCreated).ToList();
                return list;
            }
        }

        public static List<HumanResourceCv> GetHumanResourceCV(int positionId)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.HumanResourceCv.Where(d => d.HumanResourcePositionId == positionId && d.Deleted == false).OrderByDescending(d => d.TimeCreated).ToList();
                return list;
            }
        }

        public static bool DeleteCV(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.HumanResourceCv.FirstOrDefault(d => d.HumanResourceCvId == id);
                    record.Deleted = true;

                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.InsanKaynaklari.ToString();
                    logkeeper.Message = LogMessages.HumanResourcePositionCVDeleted;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.FileUrl;
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
