using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;

namespace BLL.InstituionalBL
{
    public class InstituionalManager
    {
        public static Institutional GetInstationalByLanguage(string language,int typeid)
        {
            using (MainContext db = new MainContext())
            {
                Institutional instional_info = db.Institutional.SingleOrDefault(d => d.TypeId == typeid && d.Language == language);
                return instional_info;
            }
        }

        public static bool EditInstituional(Institutional record)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    Institutional editrecord = db.Institutional.SingleOrDefault(d => d.TypeId == record.TypeId && d.Language == record.Language);

                    if (editrecord == null)
                    {
                        editrecord = new Institutional();
                        editrecord.TimeUpdated = DateTime.Now;
                        editrecord.TypeId = record.TypeId;
                        editrecord.Language = record.Language;
                        editrecord.Content = record.Content;
                        if (record.Resim != null)
                        {
                            editrecord.Resim = record.Resim;
                        }
                        editrecord.ResimTag = record.ResimTag;
                        db.Institutional.Add(editrecord);
                    }
                    else
                    {
                        editrecord.TimeUpdated = DateTime.Now;
                        editrecord.Content = record.Content;
                        if (record.Resim != null)
                        {
                            editrecord.Resim = record.Resim;
                        }
                        editrecord.ResimTag = record.ResimTag;
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
    }
}
