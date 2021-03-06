﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BLL.LogBL;
using DAL.Context;
using DAL.Entities;
using log4net;
using System.Data.Entity.Validation;

namespace BLL.ContactBL
{
    public class ContactManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static Contact GetContact(string lang)
        {
            using (MainContext db = new MainContext())
            {
                if (string.IsNullOrEmpty(lang)) lang = "tr";
                var list = db.Contact.SingleOrDefault(d=>d.Language == lang);
                return list;
            }
        }

        public static dynamic EditContact(Contact record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Contact contact = db.Contact.SingleOrDefault(d=>d.Language == record.Language);


                    if (contact == null)
                    {
                        contact = new Contact();
                        contact.Address = record.Address;
                        contact.Phone = record.Phone;
                        contact.Fax = record.Fax;
                        contact.Email = record.Email;

                        contact.Address2 = record.Address2;
                        contact.Phone2 = record.Phone2;
                        contact.Fax2 = record.Fax2;
                        contact.Email2 = record.Email2;
                        contact.Language = record.Language;
                        db.Contact.Add(contact);
                    }
                    else
                    {
                        contact.Address = record.Address;
                        contact.Phone = record.Phone;
                        contact.Fax = record.Fax;
                        contact.Email = record.Email;

                        contact.Address2 = record.Address2;
                        contact.Phone2 = record.Phone2;
                        contact.Fax2 = record.Fax2;
                        contact.Language = record.Language;
                        contact.Email2 = record.Email2;
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
               
                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    return false;

                    //throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
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
