using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using BLL.LogBL;
using System.Web;

namespace BLL.ServiceGroupBL
{
    public static class ServiceGroupManager
    {
        public static List<ServiceGroup> GetServiceGroupList(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.ServiceGroup.Where(d => d.Language == language).OrderBy(d => d.SortOrder).ToList();
                return list;
            }
        }

        public static List<T> Swap<T>(this List<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }

        public static List<ServiceGroup> GetServiceGroupListForFront(int grupID, string language)
        {
            using (MainContext db = new MainContext())
            {
                List<ServiceGroup> list = db.ServiceGroup.Where(d => d.Language == language && d.Online == true).OrderBy(d => d.SortOrder).ToList();
                if (grupID != 0)
                {
                    int index = list.Select((v, i) => new { Group = v, index = i }).First(d => d.Group.ServiceGroupId == grupID).index;
                    list = ServiceGroupManager.Swap(list, 0, index);
                }
                return list;
            }
        }

        public static bool AddServiceGroup(ServiceGroup record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.TimeCreated = DateTime.Now;
                    record.SortOrder = 9999;
                    record.Online = true;
                    db.ServiceGroup.Add(record);
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
                var list = db.ServiceGroup.SingleOrDefault(d => d.ServiceGroupId == id);
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
                    var record = db.ServiceGroup.FirstOrDefault(d => d.ServiceGroupId == id);
                    db.ServiceGroup.Remove(record);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static ServiceGroup GetServiceGroupById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ServiceGroup record = db.ServiceGroup.Where(d => d.ServiceGroupId == nid).SingleOrDefault();
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

        public static bool EditServiceGroup(ServiceGroup ServiceGroupmodel)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    ServiceGroup record = db.ServiceGroup.Where(d => d.ServiceGroupId == ServiceGroupmodel.ServiceGroupId).SingleOrDefault();
                    if (record != null)
                    {
                        record.GroupName = ServiceGroupmodel.GroupName;
                        record.PageSlug = ServiceGroupmodel.PageSlug;
                        record.Content = ServiceGroupmodel.Content;

                        record.Language = ServiceGroupmodel.Language;
                       
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
                        ServiceGroup sortingrecord = db.ServiceGroup.SingleOrDefault(d => d.ServiceGroupId == mid);
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
