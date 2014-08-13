using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;

namespace BLL.Gallery
{
    public class GalleryManager
    {
        #region GalleryGroup
        public static List<GalleryGroup> GetGalleryGroupList(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.GalleryGroup.Where(d => d.Deleted == false && d.Language == language).OrderBy(d=>d.SortOrder).ToList();
                return list;
            }
        }

        public static bool AddGalleryGroup(GalleryGroup record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    //record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    record.Online = true;
                   record.SortOrder = 9999;
                    db.GalleryGroup.Add(record);
                    db.SaveChanges();

                    //LogtrackManager logkeeper = new LogtrackManager();
                    //logkeeper.LogDate = DateTime.Now;
                    //logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                    //logkeeper.Message = LogMessages.GalleryGroupAdded;
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
                        GalleryGroup sortingrecord = db.GalleryGroup.SingleOrDefault(d => d.GalleryGroupId == mid);
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


        public static bool UpdateGroupStatus(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.GalleryGroup.SingleOrDefault(d => d.GalleryGroupId == id);
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
                    var record = db.GalleryGroup.FirstOrDefault(d => d.GalleryGroupId == id);
                    record.Deleted = true;

                    db.SaveChanges();

                    //LogtrackManager logkeeper = new LogtrackManager();
                    //logkeeper.LogDate = DateTime.Now;
                    //logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                    //logkeeper.Message = LogMessages.GalleryGroupDeleted;
                    //logkeeper.User = HttpContext.Current.User.Identity.Name;
                    //logkeeper.Data = record.GroupName;
                    //logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static GalleryGroup GetGalleryGroupById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    GalleryGroup record = db.GalleryGroup.Where(d => d.GalleryGroupId == nid).SingleOrDefault();
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

        public static bool EditGalleryGroup(int id, string name, string pageslug)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    GalleryGroup record = db.GalleryGroup.Where(d => d.GalleryGroupId == id && d.Deleted == false).SingleOrDefault();
                    if (record != null)
                    {

                        record.GroupName = name;
                        //record.PageSlug = pageslug;
                     //   record.TimeUpdated = DateTime.Now;
                        db.SaveChanges();

                        //LogtrackManager logkeeper = new LogtrackManager();
                        //logkeeper.LogDate = DateTime.Now;
                        //logkeeper.LogProcess = EnumLogType.DokumanGrup.ToString();
                        //logkeeper.Message = LogMessages.GalleryGroupEdited;
                        //logkeeper.User = HttpContext.Current.User.Identity.Name;
                        //logkeeper.Data = record.GroupName;
                        //logkeeper.AddInfoLog(logger);

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


      

        #endregion GalleryGroup


        #region Gallery

    

        public static object DeleteImage(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.Gallery.FirstOrDefault(d => d.GalleryId == id);
                    db.Gallery.Remove(record);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static DAL.Entities.Gallery GetGalleryById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    DAL.Entities.Gallery record = db.Gallery.Where(d => d.GalleryId == nid).SingleOrDefault();
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

        public static List<DAL.Entities.Gallery> GetGalleryList(int gid)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Gallery.Where(d => d.GalleryGroupId == gid).ToList();
                return list;
            }
        }
        #endregion Gallery

    }
}
