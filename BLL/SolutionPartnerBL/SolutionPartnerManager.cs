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

namespace BLL.SolutionPartnerBL
{
    public class SolutionPartnerManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<SolutionPartner> GetSolutionPartnerList(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.SolutionPartner.Where(d => d.Deleted == false && d.Language == language).OrderBy(d=>d.SortOrder).ToList();
                return list;
            }
        }

        public static List<SolutionPartner> GetSolutionPartnerListForFront(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.SolutionPartner.Where(d => d.Deleted == false && d.Language == language && d.Online==true).OrderBy(d=>d.SortOrder).ToList();
                return list;
            }
        }

        public static bool AddSolutionPartner(SolutionPartner record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    if (!record.TimeCreated.HasValue)
                        record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    record.Online = true;
                    db.SolutionPartner.Add(record);
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.CozumOrtaklari.ToString();
                    logkeeper.Message = LogMessages.SolutionPartnerAdded;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.SolutionPartnerName;
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
                var list = db.SolutionPartner.SingleOrDefault(d => d.SolutionPartnerId == id);
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
                    var record = db.SolutionPartner.FirstOrDefault(d => d.SolutionPartnerId == id);
                    record.Deleted = true;
                    
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.CozumOrtaklari.ToString();
                    logkeeper.Message = LogMessages.SolutionPartnerDeleted;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.SolutionPartnerName;
                    logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static SolutionPartner GetSolutionPartnerById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    SolutionPartner record = db.SolutionPartner.Where(d => d.SolutionPartnerId == nid).SingleOrDefault();
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

        public static bool EditSolutionPartner(SolutionPartner SolutionPartnermodel)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    SolutionPartner record = db.SolutionPartner.Where(d => d.SolutionPartnerId == SolutionPartnermodel.SolutionPartnerId && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        record.Content = SolutionPartnermodel.Content;
                        record.Link = SolutionPartnermodel.Link;
                        record.SolutionPartnerName = SolutionPartnermodel.SolutionPartnerName;
                        record.Language = SolutionPartnermodel.Language;
                        if (!string.IsNullOrEmpty(SolutionPartnermodel.Logo))
                        {
                            record.Logo = SolutionPartnermodel.Logo;
                        }
                        record.Content = SolutionPartnermodel.Content;
                        db.SaveChanges();

                        LogtrackManager logkeeper = new LogtrackManager();
                        logkeeper.LogDate = DateTime.Now;
                        logkeeper.LogProcess = EnumLogType.CozumOrtaklari.ToString();
                        logkeeper.Message = LogMessages.SolutionPartnerEdited;
                        logkeeper.User = HttpContext.Current.User.Identity.Name;
                        logkeeper.Data = record.SolutionPartnerName;
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
                        SolutionPartner sortingrecord = db.SolutionPartner.SingleOrDefault(d => d.SolutionPartnerId == mid);
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
