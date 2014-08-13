using BLL.LogBL;
using DAL.Context;
using DAL.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.EstateBL
{
    public class EstateManager
    {
        #region Country
        /************
         * İL
         * İLÇE
         * SEMTT 
        */
        public static List<Country> GetCountryList()
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Country.ToList();
                return list;
            }
        }

        public static List<Town> GetTownList(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Town.Where(x=>x.CountryId==id).ToList();
                return list;
            }
        }

        public static List<District> GetDistrictList(int id)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.District.Where(x => x.TownId == id).ToList();
                return list;
            }
        }

      

        #endregion Country

        #region Estate

        static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool AddEstate(Estate record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {

                    db.Estate.Add(record);
                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Emlak.ToString();
                    logkeeper.Message = LogMessages.EstateAdded;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.Header;
                    logkeeper.AddInfoLog(logger);


                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public static List<Estate> GetEstateList(string language)
        {
            using (MainContext db = new MainContext())
            {
                var list = db.Estate.Include("Country").Include("Town").Include("District").Where(d => d.Language == language).ToList();
                return list;
            }
        }

        public static bool Delete(int id)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var record = db.Estate.FirstOrDefault(d => d.Id == id);
                    db.Estate.Remove(record);

                    db.SaveChanges();

                    LogtrackManager logkeeper = new LogtrackManager();
                    logkeeper.LogDate = DateTime.Now;
                    logkeeper.LogProcess = EnumLogType.Emlak.ToString();
                    logkeeper.Message = LogMessages.EstateDeleted;
                    logkeeper.User = HttpContext.Current.User.Identity.Name;
                    logkeeper.Data = record.Header;
                    logkeeper.AddInfoLog(logger);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static Estate GetEstateById(int nid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Estate record = db.Estate.Where(d => d.Id == nid).SingleOrDefault();
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

        public static bool EditEstate(Estate model)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Estate record = db.Estate.Where(d => d.Id == model.Id).SingleOrDefault();
                    if (record != null)
                    {
                        record.Header = model.Header;
                        record.Language = model.Language;
                        record.Content = model.Content;

                        record.Price = model.Price;
                        record.PriceTypeId = model.PriceTypeId;
                        record.ReferenceNo = model.ReferenceNo;
                        record.RoomNumber = model.RoomNumber;
                        record.Size = model.Size;
                        record.TimeCreated = model.TimeCreated;
                        record.Consultant = model.Consultant;
                        record.Age = model.Age;
                        record.CountryId = model.CountryId;
                        record.TownId = model.TownId;
                        record.DistrictId = model.DistrictId;
                        record.Popular = model.Popular;
                        record.TypeId = model.TypeId;
                        record.TransactionId = model.TransactionId;

                        record.NetAlan = model.NetAlan;
                        record.BulunduguArsa = model.BulunduguArsa;
                        record.BinaTabanAlani = model.BinaTabanAlani;
                        record.BanyoWc = model.BanyoWc;
                        record.BinaKatSayisi = model.BinaKatSayisi;
                        record.BulunduguKat = model.BulunduguKat;
                        record.CepheYon = model.CepheYon;
                        record.IsınmaTipi = model.IsınmaTipi;
                        record.YakitTipi = model.YakitTipi;
                        record.Otopark = model.Otopark;
                        record.YapininDurumu = model.YapininDurumu;
                        record.KullanimDurumu = model.KullanimDurumu;
                        record.TapuDurumu = model.TapuDurumu;
                        record.Depozito = model.Depozito;
                        record.AidatYonetim = model.AidatYonetim;
                        record.Devren = model.Devren;
                        record.Takas = model.Takas;
                        record.KrediDurumu = model.KrediDurumu;
                        record.KiraGetirisi = model.KiraGetirisi;

                        if (!string.IsNullOrEmpty(model.Photo))
                        {
                            record.Photo = model.Photo;
                        }

                        if (!string.IsNullOrEmpty(model.EmlakDosyasi))
                        {
                            record.EmlakDosyasi = model.EmlakDosyasi;
                        }


                        db.SaveChanges();

                        LogtrackManager logkeeper = new LogtrackManager();
                        logkeeper.LogDate = DateTime.Now;
                        logkeeper.LogProcess = EnumLogType.Emlak.ToString();
                        logkeeper.Message = LogMessages.EstateEdited;
                        logkeeper.User = HttpContext.Current.User.Identity.Name;
                        logkeeper.Data = record.Header;
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


        #endregion Estate
    }
}
