using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using BLL.LogBL;
using System.Web;

namespace BLL.SectorGroupBL
{
    public class SectorGroupManager
    {
        public static List<SectorGroup> GetSectorGroupList(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.SectorGroup.Where(d => d.Language == language).OrderBy(d => d.SortOrder).ToList();
                return list;
            }
        }

        public static List<SectorGroup> GetSectorGroupListForFront(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.SectorGroup.Where(d => d.Language == language && d.Online == true).OrderBy(d => d.SortOrder).ToList();
                return list;
            }
        }

        public static bool AddSectorGroup(SectorGroup record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.TimeCreated = DateTime.Now;
                    record.SortOrder = 9999;
                    record.Online = true;
                    db.SectorGroup.Add(record);
                    db.SaveChanges();

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
                var list = db.SectorGroup.SingleOrDefault(d => d.SectorGroupId == id);
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
                    var record = db.SectorGroup.FirstOrDefault(d => d.SectorGroupId == id);
                    db.SectorGroup.Remove(record);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static SectorGroup GetSectorGroupById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    SectorGroup record = db.SectorGroup.Where(d => d.SectorGroupId == nid).SingleOrDefault();
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

        public static bool EditSectorGroup(SectorGroup SectorGroupmodel)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    SectorGroup record = db.SectorGroup.Where(d => d.SectorGroupId == SectorGroupmodel.SectorGroupId).SingleOrDefault();
                    if (record != null)
                    {
                        record.GroupName = SectorGroupmodel.GroupName;
                        record.PageSlug = SectorGroupmodel.PageSlug;
                        record.GroupContent = SectorGroupmodel.GroupContent;
                        if (SectorGroupmodel.SectorGroupLogo != null)
                        {
                            record.SectorGroupLogo = SectorGroupmodel.SectorGroupLogo;                            
                        }
                        record.Language = SectorGroupmodel.Language;
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
                        SectorGroup sortingrecord = db.SectorGroup.SingleOrDefault(d => d.SectorGroupId == mid);
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
