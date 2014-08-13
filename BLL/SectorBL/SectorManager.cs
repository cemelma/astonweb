using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using BLL.LogBL;
using System.Web;

namespace BLL.SectorBL
{
    public class SectorManager
    {
        public static List<Sector> GetSectorList(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Sector.Where(d => d.SectorGroupId == id).OrderBy(d => d.SortOrder).ToList();
                return list;
            }
        }

        public static OurSectors GetOurSectors(string language)
        {
            using (MainContext db = new MainContext())
            {
                OurSectors model = db.OurSectors.SingleOrDefault(d => d.Language == language);
                return model;
            }
        }

        public static bool EditOurSectors(OurSectors record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    OurSectors editrecord = db.OurSectors.SingleOrDefault(d=> d.Language == record.Language);

                    if (editrecord == null)
                    {
                        editrecord = new OurSectors();
                        editrecord.TimeUpdated = DateTime.Now;
                        editrecord.Language = record.Language;
                        editrecord.Content = record.Content;
                        db.OurSectors.Add(editrecord);
                    }
                    else
                    {
                        editrecord.TimeUpdated = DateTime.Now;
                        editrecord.Content = record.Content;
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

        public static List<Sector> GetSectorListForFront(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Sector.Where(d => d.Language == language && d.Online == true).OrderBy(d => d.SortOrder).ToList();
                return list;
            }
        }

        public static bool AddSector(Sector record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.TimeCreated = DateTime.Now;
                    record.SortOrder = 9999;
                    record.Online = true;
                    db.Sector.Add(record);
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
                var list = db.Sector.SingleOrDefault(d => d.SectorId == id);
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
                    var record = db.Sector.FirstOrDefault(d => d.SectorId == id);
                    db.Sector.Remove(record);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static Sector GetSectorById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Sector record = db.Sector.Where(d => d.SectorId == nid).SingleOrDefault();
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

        public static bool EditSector(Sector Sectormodel)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Sector record = db.Sector.Where(d => d.SectorId == Sectormodel.SectorId).SingleOrDefault();
                    if (record != null)
                    {
                        record.Name = Sectormodel.Name;
                        record.SectorGroupId = Sectormodel.SectorGroupId;
                        record.PageSlug = Sectormodel.PageSlug;
                        record.Language = Sectormodel.Language;
                       
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
                        Sector sortingrecord = db.Sector.SingleOrDefault(d => d.SectorId == mid);
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
