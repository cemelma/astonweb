using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.SocialMediaBL
{
    public class SocialMediaManager
    {
        public static List<SocialMedia> GetList()
        {
            using (MainContext db = new MainContext())
            {

                var list = db.SocialMedia.OrderBy(x=>x.SortOrder).ToList();
               return list;
            }
        }


        public static bool AddSocialMedia(SocialMedia record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.SortOrder = 9999;
                    db.SocialMedia.Add(record);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public static SocialMedia GetSocialMediaById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    SocialMedia record = db.SocialMedia.Where(d => d.Id == nid).SingleOrDefault();
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

        public static bool EditMedia(SocialMedia model)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    SocialMedia record = db.SocialMedia.Where(d => d.Id == model.Id).SingleOrDefault();
                    if (record != null)
                    {
                        record.LinkName = model.LinkName;
                        record.Name = model.Name;
                     
                        if (!string.IsNullOrEmpty(model.Logo))
                        {
                            record.Logo = model.Logo;
                        }

                        db.SaveChanges();
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


        public static bool Delete(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.SocialMedia.FirstOrDefault(d => d.Id == id);
                    db.SocialMedia.Remove(record);

                    db.SaveChanges();

                  

                    return true;
                }
                catch (Exception)
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
                        SocialMedia sortingrecord = db.SocialMedia.SingleOrDefault(d => d.Id == mid);
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
