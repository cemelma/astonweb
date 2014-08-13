using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;

namespace BLL.MailBL
{
    public class MailManager
    {
       
        public static List<MailUsers> GetMailUsersList(int type)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.MailUsers.Where(d => d.MailType == type).ToList();
                return list;
            }
        }


        public static bool AddMailUsers(MailUsers record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    db.MailUsers.Add(record);
                    db.SaveChanges();
                    return true;
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
                    var record = db.MailUsers.FirstOrDefault(d => d.MailUserId == id);
                    db.MailUsers.Remove(record);

                    db.SaveChanges();

                 

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static MailUsers GetMailUsersById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    MailUsers record = db.MailUsers.Where(d => d.MailUserId == nid).SingleOrDefault();
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

        public static bool EditMailUser(MailUsers model)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    MailUsers record = db.MailUsers.Where(d => d.MailUserId == model.MailUserId).SingleOrDefault();
                    if (record != null)
                    {
                        record.MailType = model.MailType;
                        record.MailUser = model.MailUser;
                        record.MailAddress = model.MailAddress;
           
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


        public static MailSetting GetMailSettings()
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    MailSetting record = db.MailSetting.SingleOrDefault();
                    if (record != null)
                    {
                        return record;
                    }
                    else
                        return null;

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static dynamic AddSetting(MailSetting model)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    db.MailSetting.Add(model);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }



        public static bool EditSetting(MailSetting model)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    MailSetting editrec = db.MailSetting.Where(d => d.MailSettingId == model.MailSettingId).SingleOrDefault();
                    if (editrec != null)
                    {
                        editrec.ServerMail = model.ServerMail;
                        editrec.ServerHost = model.ServerHost;
                        editrec.Port = model.Port;
                        editrec.Security = model.Security;
                        if (!string.IsNullOrEmpty(model.Password))
                        {
                            editrec.Password = model.Password;
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



    }
}
