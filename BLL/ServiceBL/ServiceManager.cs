using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using BLL.LogBL;
using System.Web;

namespace BLL.ServiceBL
{
    public class ServiceManager
    {
        public static List<Service> GetServiceList(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Service.Where(d => d.ServiceGroupId == id).OrderBy(d => d.SortOrder).ToList();
                return list;
            }
        }

        public static List<Service> GetServiceList()
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Service.Where(x=>x.Online==true && x.Deleted==false && x.ShowInMenu==true).OrderBy(d => d.SortOrder).ToList();
                return list;
            }
        }

        public static OurServices GetOurServices(string language)
        {
            using (MainContext db = new MainContext())
            {
                OurServices model = db.OurServices.SingleOrDefault(d => d.Language == language);
                return model;
            }
        }

        public static bool EditOurServices(OurServices record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    OurServices editrecord = db.OurServices.SingleOrDefault(d=> d.Language == record.Language);

                    if (editrecord == null)
                    {
                        editrecord = new OurServices();
                        editrecord.TimeUpdated = DateTime.Now;
                        editrecord.Language = record.Language;
                        editrecord.Content = record.Content;
                        db.OurServices.Add(editrecord);
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

        public static List<Service> GetServiceListForFront(string language)
        {
            using (MainContext db = new MainContext())
            {// && d.ShowInMenu==true
                var list = db.Service.Where(d =>  d.Online == true).OrderBy(d => d.SortOrder).ToList();
                return list;
            }
        }

        public static bool AddService(Service record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.TimeCreated = DateTime.Now;
                    record.SortOrder = 9999;
                    record.Online = true;
                    db.Service.Add(record);
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
                var list = db.Service.SingleOrDefault(d => d.ServiceId == id);
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
                    var record = db.Service.FirstOrDefault(d => d.ServiceId == id);
                    db.Service.Remove(record);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static Service GetServiceById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Service record = db.Service.Where(d => d.ServiceId == nid).SingleOrDefault();
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

        public static bool EditService(Service Servicemodel)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Service record = db.Service.Where(d => d.ServiceId == Servicemodel.ServiceId).SingleOrDefault();
                    if (record != null)
                    {
                        record.Name = Servicemodel.Name;
                        record.ServiceGroupId = Servicemodel.ServiceGroupId;
                        record.PageSlug = Servicemodel.PageSlug;
                        record.Language = Servicemodel.Language;
                        record.Content = Servicemodel.Content;
                        record.ShowContentHome = Servicemodel.ShowContentHome;
                        record.ShowInMenu = Servicemodel.ShowInMenu;
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
                        Service sortingrecord = db.Service.SingleOrDefault(d => d.ServiceId == mid);
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
