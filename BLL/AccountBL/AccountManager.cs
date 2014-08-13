using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myBLOGData;
using System.Data;
using System.Web.Security;
using System.Web;
using DAL.Context;
using DAL.Entities;
using BLL.LogBL;
using log4net;
namespace BLL.AccountBL
{
    public class AccountManager
    {
        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public static bool Login(string email, string password)
        {
            using(MainContext db=new MainContext())
            {
                AdminUser record = db.AdminUser.SingleOrDefault(d => d.Email == email && d.Password == password);
                if (record != null)
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, record.FullName, DateTime.Now, DateTime.Now.AddMinutes(120), false, "Admin", FormsAuthentication.FormsCookiePath);
                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

                    HttpContext.Current.Response.Cookies.Add(cookie);
                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Login.ToString();
                    logkeeper.Message = LogMessages.Logined;
                    logkeeper.User = record.FullName;
                    logkeeper.Data = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    logkeeper.AddInfoLog(logger);
                    return true;
                }
                else
                {
                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Login.ToString();
                    logkeeper.Message = LogMessages.NotLogined;
                    logkeeper.User = email;
                    logkeeper.Data = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    logkeeper.AddInfoLog(logger);
                    return false;
                }
            }
        }

        public static bool AddNewUser(AdminUser record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    db.AdminUser.Add(record);
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Kullanici.ToString();
                    logkeeper.Message = LogMessages.UserAdded;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.FullName;
                    logkeeper.AddInfoLog(logger);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public static List<AdminUser> GetUserList()
        {
            using (MainContext db = new MainContext())
            {
                var list = db.AdminUser.ToList();
                return list;
            }
        }

        public static List<User> GetCustomerList()
        {
            using (MainContext db = new MainContext())
            {
                var list = db.User.Where(d=>d.Deleted == false).ToList();
                return list;
            }
        }

        public static bool Delete(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.AdminUser.FirstOrDefault(d => d.AdminUserId == id);
                    db.AdminUser.Remove(record);

                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Kullanici.ToString();
                    logkeeper.Message = LogMessages.UserDeleted;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.FullName;
                    logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static object UpdateStatus(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.User.SingleOrDefault(d => d.UserId == id);
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

        public static bool DeleteUsers(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.User.FirstOrDefault(d => d.UserId == id);
                    record.Deleted = true;

                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Kullanici.ToString();
                    logkeeper.Message = LogMessages.UserDeletedFront;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.FullName;
                    logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static AdminUser GetUserInfoById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    AdminUser record = db.AdminUser.Where(d => d.AdminUserId == nid).SingleOrDefault();
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


        public static bool EditUser(AdminUser model)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    AdminUser record = db.AdminUser.Where(d => d.AdminUserId == model.AdminUserId).SingleOrDefault();
                    if (record != null)
                    {
                        record.FullName = model.FullName;
                        record.Email = model.Email;

                        if (!string.IsNullOrEmpty(model.Password))
                        {
                            record.Password = model.Password;
                        }

                        db.SaveChanges();

                        LogtrackManager logkeeper = new LogtrackManager();
                        logkeeper.LogDate = DateTime.Now;
                        logkeeper.LogProcess = EnumLogType.Kullanici.ToString();
                        logkeeper.Message = LogMessages.UserEdited;
                        logkeeper.User = HttpContext.Current.User.Identity.Name;
                        logkeeper.Data = record.FullName;
                        logkeeper.AddInfoLog(logger);

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
