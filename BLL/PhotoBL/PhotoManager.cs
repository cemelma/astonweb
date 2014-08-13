using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.PhotoBL
{
    public class PhotoManager
    {
        public static bool Save(Photo p)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    db.Photo.Add(p);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static List<Photo> GetList(int categoryID, int itemId)
        {
            using (MainContext db = new MainContext())
            {
                return db.Photo.Where(d => d.ItemId == itemId && d.CategoryId == categoryID).OrderBy(d => d.SortOrder).ToList();
            }
        }

        public static List<Photo> GetList(string lang, int categoryID)
        {
            using (MainContext db = new MainContext())
            {
                return db.Photo.Where(d => d.Language == lang && d.CategoryId == categoryID).OrderBy(d => d.SortOrder).ToList();
            }
        }

        public static List<Photo> GetList(int categoryID)
        {
            using (MainContext db = new MainContext())
            {
                return db.Photo.Where(d => d.CategoryId == categoryID).OrderBy(d => d.SortOrder).OrderBy(d => d.SortOrder).ToList();
            }
        }

        public static List<Photo> GetListForFront(int categoryID, string lang)
        {
            using (MainContext db = new MainContext())
            {
                return db.Photo.Where(d => d.CategoryId == categoryID && d.Language == lang && d.Online == true).OrderBy(d => d.SortOrder).OrderBy(d => d.SortOrder).ToList();
            }
        }

        public static List<Photo> GetListForFront(int categoryID)
        {
            using (MainContext db = new MainContext())
            {
                return db.Photo.Where(d => d.CategoryId == categoryID).OrderBy(d => d.SortOrder).OrderBy(d => d.SortOrder).ToList();
            }
        }

        public static List<Photo> GetListForFront(int categoryID, int ItemId)
        {
            using (MainContext db = new MainContext())
            {
                return db.Photo.Where(d => d.CategoryId == categoryID && d.ItemId == ItemId).OrderBy(d => d.SortOrder).OrderBy(d => d.SortOrder).ToList();
            }
        }

        public static bool Delete(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Photo p = db.Photo.First(d => d.PhotoId == id);
                    db.Photo.Remove(p);
                    string filePath = HttpContext.Current.Server.MapPath(p.Path);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    string filePatht = HttpContext.Current.Server.MapPath(p.Thumbnail);
                    if (System.IO.File.Exists(filePatht))
                    {
                        System.IO.File.Delete(filePatht);
                    }

                    db.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static bool Edit(int id, string Title, string path,string link)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Photo p = db.Photo.First(d => d.PhotoId == id);
                    p.Title = Title;
                    p.Link = link;
                    if (path != null)
                    {
                        p.Path = path;
                    }
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static Photo GetById(int id)
        {
            using (MainContext db = new MainContext())
            {
                return db.Photo.First(d => d.PhotoId == id);
            }
        }

        public static void UpdatePhotoPath(int id, string path)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Photo.SingleOrDefault(d => d.PhotoId == id);
                try
                {
                    if (list != null)
                    {
                        list.Thumbnail = path;
                        db.SaveChanges();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public static bool UpdateStatus(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Photo.SingleOrDefault(d => d.PhotoId == id);
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
                        Photo sortingrecord = db.Photo.SingleOrDefault(d => d.PhotoId == mid);
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

        public static List<Photo> GetListForFront(string lang, int categoryID)
        {
            using (MainContext db = new MainContext())
            {
                return db.Photo.Where(d => d.Language == lang && d.CategoryId == categoryID && d.Online == true).OrderBy(d=>d.SortOrder).ToList();
            }
        }

        public static List<Photo> GetListForFrontServices(int categoryID)
        {
            using (MainContext db = new MainContext())
            {
                return db.Photo.Where(d => d.Language == "tr" && d.CategoryId == categoryID && d.Online == true).OrderBy(d => d.SortOrder).ToList();
            }
        }

    }
}
