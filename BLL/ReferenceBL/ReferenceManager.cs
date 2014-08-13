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

namespace BLL.ReferenceBL
{
    public class ReferenceManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<References> GetReferenceList(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.References.Where(d => d.Deleted == false && d.Language == language).OrderBy(d=>d.SortOrder).ToList();
                return list;
            }
        }

        public static List<References> GetReferenceListForFront(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.References.Where(d => d.Deleted == false && d.Language == language && d.Online==true).OrderBy(d=>d.SortOrder).ToList();
                return list;
            }
        }

        public static bool AddReference(References record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    if (!record.TimeCreated.HasValue)
                        record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    record.Online = true;
                    db.References.Add(record);
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Referans.ToString();
                    logkeeper.Message = LogMessages.ReferenceAdded;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.ReferenceName;
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
                var list = db.References.SingleOrDefault(d => d.ReferenceId == id);
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
                    var record = db.References.FirstOrDefault(d => d.ReferenceId == id);
                    record.Deleted = true;
                    
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Referans.ToString();
                    logkeeper.Message = LogMessages.ReferenceDeleted;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.ReferenceName;
                    logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static References GetReferenceById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    References record = db.References.Where(d => d.ReferenceId == nid).SingleOrDefault();
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

        public static bool EditReference(References referencemodel)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    References record = db.References.Where(d => d.ReferenceId == referencemodel.ReferenceId && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        record.Content = referencemodel.Content;
                        record.Link = referencemodel.Link;
                        record.ReferenceName = referencemodel.ReferenceName;
                        record.Language = referencemodel.Language;
                        if (!string.IsNullOrEmpty(referencemodel.Logo))
                        {
                            record.Logo = referencemodel.Logo;
                        }
                        record.Content = referencemodel.Content;
                        db.SaveChanges();

                        LogtrackManager logkeeper = new LogtrackManager();
                        logkeeper.LogDate = DateTime.Now;
                        logkeeper.LogProcess = EnumLogType.Referans.ToString();
                        logkeeper.Message = LogMessages.ReferenceEdited;
                        logkeeper.User = HttpContext.Current.User.Identity.Name;
                        logkeeper.Data = record.ReferenceName;
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
                        References sortingrecord = db.References.SingleOrDefault(d => d.ReferenceId == mid);
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
