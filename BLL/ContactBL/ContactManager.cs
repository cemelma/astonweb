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

namespace BLL.ContactBL
{
    public class ContactManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static Contact GetContact()
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Contact.SingleOrDefault();
                return list;
            }
        }

        public static dynamic EditContact(Contact record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Contact contact = db.Contact.SingleOrDefault();
                    if (contact == null)
                    {
                        contact = new Contact();
                        contact.Address = record.Address;
                        contact.Phone = record.Phone;
                        contact.Fax = record.Fax;
                        contact.Email = record.Email;
                        db.Contact.Add(contact);
                    }
                    else
                    {
                        contact.Address = record.Address;
                        contact.Phone = record.Phone;
                        contact.Fax = record.Fax;
                        contact.Email = record.Email;
                    }

                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Contact.ToString();
                    logkeeper.Message = LogMessages.ContactEdited;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.Address;
                    logkeeper.AddInfoLog(logger);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }






        public static bool AddContactHome(ContactHome record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    record.TimeCreated = DateTime.Now;
                    record.Deleted = false;
                    db.ContactHome.Add(record);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static List<ContactHome> GetListContactHome()
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    return db.ContactHome.Where(d => d.Deleted == false).OrderBy(d => d.TimeCreated).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static bool Delete(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.ContactHome.FirstOrDefault(d => d.ContactId == id);
                    if (record != null)
                        record.Deleted = true;
                    db.SaveChanges();
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
