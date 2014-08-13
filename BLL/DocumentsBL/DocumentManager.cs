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

namespace BLL.DocumentsBL
{
    public class DocumentManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #region documentgroup
        public static List<DocumentGroup> GetDocumentGroupList(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.DocumentGroup.Where(d => d.Deleted == false && d.Language == language).OrderBy(d=>d.SortNumber).ToList();
                return list;
            }
        }

        public static List<DocumentGroup> GetDocumentGroupListForFront(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.DocumentGroup.Where(d => d.Deleted == false && d.Language == language && d.Online==true).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }


        public static bool AddDocumentGroup(DocumentGroup record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    record.Online = true;
                    record.SortNumber = 9999;
                    db.DocumentGroup.Add(record);
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                    logkeeper.Message = LogMessages.DocumentGroupAdded;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.GroupName;
                    logkeeper.AddInfoLog(logger);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }


        public static bool UpdateGroupStatus(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.DocumentGroup.SingleOrDefault(d => d.DocumentGroupId == id);
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


        public static bool DeleteGroup(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.DocumentGroup.FirstOrDefault(d => d.DocumentGroupId == id);
                    record.Deleted = true;

                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                    logkeeper.Message = LogMessages.DocumentGroupDeleted;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.GroupName;
                    logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static DocumentGroup GetDocumentGroupById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    DocumentGroup record = db.DocumentGroup.Where(d => d.DocumentGroupId == nid).SingleOrDefault();
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

        public static bool EditDocumentGroup(int id, string name,string pageslug)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    DocumentGroup record = db.DocumentGroup.Where(d => d.DocumentGroupId == id && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {

                        record.GroupName = name;
                        record.PageSlug = pageslug;
                        record.TimeUpdated = DateTime.Now;
                        db.SaveChanges();

                        LogtrackManager logkeeper = new LogtrackManager();
                        logkeeper.LogDate = DateTime.Now;
                        logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                        logkeeper.Message = LogMessages.DocumentGroupEdited;
                        logkeeper.User = HttpContext.Current.User.Identity.Name;
                        logkeeper.Data = record.GroupName;
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
                        DocumentGroup sortingrecord = db.DocumentGroup.SingleOrDefault(d => d.DocumentGroupId == mid);
                        sortingrecord.SortNumber = Convert.ToInt32(row);
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

        #endregion documentgroup

        #region document



        public static List<Document> GetDocumentList(int gid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Document.Where(d => d.Deleted == false && d.DocumentGroupId == gid).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }

        public static List<Document> GetDocumentListForFront(int gid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Document.Where(d => d.Deleted == false && d.DocumentGroupId == gid && d.Online==true).OrderBy(d => d.SortNumber).ToList();
                return list;
            }
        }

        public static bool AddDocument(Document record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                   
                    record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    
                    record.Online = true;
                    record.SortNumber = 9999;
                    db.Document.Add(record);
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Dokuman.ToString();
                    logkeeper.Message = LogMessages.DocumentAdded;
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

        public static bool EditDocument(Document data)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Document record = db.Document.Where(d => d.DocumentId == data.DocumentId && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {
                        record.Name = data.Name;
                        record.Language = data.Language;
                        record.DocumentGroupId = data.DocumentGroupId;
                        if (!string.IsNullOrEmpty(data.DocumentFile))
                        {
                            record.DocumentFile = data.DocumentFile;
                        }
                        record.TimeUpdated = DateTime.Now;

                        db.SaveChanges();

                        LogtrackManager logkeeper = new LogtrackManager();
                        logkeeper.LogDate = DateTime.Now;
                        logkeeper.LogProcess = EnumLogType.Dokuman.ToString();
                        logkeeper.Message = LogMessages.DocumentEdited;
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


        #endregion document


        public static object UpdateStatus(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Document.SingleOrDefault(d => d.DocumentId == id);
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

        public static object DeleteDocument(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.Document.FirstOrDefault(d => d.DocumentId == id);
                    record.Deleted = true;

                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Dokuman.ToString();
                    logkeeper.Message = LogMessages.DocumentDeleted;
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

        public static Document GetDocumentById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Document record = db.Document.Where(d => d.DocumentId == nid && d.Deleted==false).SingleOrDefault();
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



        public static bool SortDocuments(string[] idsList)
        {
            using (MainContext db = new MainContext())
            {
                try
                {

                    int row = 0;
                    foreach (string id in idsList)
                    {
                        int mid = Convert.ToInt32(id);
                        Document sortingrecord = db.Document.SingleOrDefault(d => d.DocumentId == mid);
                        sortingrecord.SortNumber = Convert.ToInt32(row);
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

