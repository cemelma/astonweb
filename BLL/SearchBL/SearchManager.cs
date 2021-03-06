﻿using DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.SearchBL
{
    public class SearchManager
    {
        public static List<Tuple<string, string>> Search(string text)
        {
            string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();
            
            using (MainContext db = new MainContext())
            {
                //var references = db.ProjectReferences.Where(d => d.Online == true && d.Language == lang).FullTextSearch(text);
                //var servicegs = db.ServiceGroup.Where(d => d.Online == true && d.Deleted == false && d.Language == lang).FullTextSearch(text);
                //var services = db.Service.Where(d => d.Online == true && d.Deleted == false && d.Language == lang).FullTextSearch(text);
                //var sectorgs = db.SectorGroup.Where(d => d.Online == true && d.Deleted == false && d.Language == lang).FullTextSearch(text);
                //var sectors = db.Sector.Where(d => d.Online == true && d.Deleted == false && d.Language == lang).FullTextSearch(text);
                var news = db.News.Where(d => d.Online == true && d.Deleted == false && d.Language == lang).FullTextSearch(text);
                var products = db.Product.Where(d => d.Online == true && d.Deleted == false && d.Language == lang).FullTextSearch(text);
                var inst = db.Institutional.Where(d => d.Language == lang).FullTextSearch(text);
                
                var result = new List<Tuple<string, string>>();
                string route, link = string.Empty;

                ///urunler/68/muflu-betornarme-boru-grubu
                foreach (var item in products)
                {
                    if (lang.Equals("tr")) route = "urunler"; else route = "products";
                    link = "/" + lang + "/" + route + "/" + item.ProductId + "/" + item.PageSlug;
                    result.Add(Tuple.Create(item.Name, link));
                }

                //if (text.ToLower().Contains("iletisim") || text.ToLower().Contains("iletişim"))
                //    result.Add(Tuple.Create("İletişim", "/" + lang + "/iletisim"));

                foreach (var item in news)
                {
                    if (lang.Equals("tr")) route = "haberler"; else route = "news";
                    link = "/" + lang + "/" + route;
                    result.Add(Tuple.Create(item.Header, link));
                }

                foreach (var item in inst)
                {
                    if (lang.Equals("tr")) route = "kurumsal"; else route = "institutional";
                    link = "/" + lang + "/" + route;
                    string header="";
                    if (lang.Equals("tr")) header = "Kurumsal";
                    else header = "Institutional";
                    result.Add(Tuple.Create(header, link));
                }

                //if (text.ToLower().Contains("kurumsal") || text.ToLower().Contains("kurumsal"))
                //    result.Add(Tuple.Create("Kurumsal", "/" + lang + "/kurumsal"));

                //foreach (var item in services)
                //{
                //    route = "hizmetler";
                //    link = "/" + route + "/" + item.ServiceId + "/" + item.PageSlug;
                //    result.Add(Tuple.Create(item.Name, link));
                //}

                //foreach (var item in sectorgs)
                //{
                //    if (lang.Equals("tr")) route = "sektorler"; else route = "sectors";
                //    link = "/" + lang + "/" + route;
                //    result.Add(Tuple.Create(item.GroupName, link));
                //}

                //foreach (var item in sectors)
                //{
                //    if (lang.Equals("tr")) route = "sektorler"; else route = "sectors";
                //    link = "/" + lang + "/" + route + "/" + item.PageSlug + "/" + item.SectorGroupId +"/" + item.SectorId;
                //    result.Add(Tuple.Create(item.Name, link));
                //}

               


                //foreach (var item in emlak)
                //{
                //    if (lang.Equals("tr"))
                //        route = "detay";
                //    else
                //        route = "detail";

                //    var prod = EstateBL.EstateManager.GetEstateById(item.Id);

                //    if (prod != null)
                //    {
                //        link = "/" + lang + "/" + route + "/" + item.Id;

                //        result.Add(Tuple.Create(item.Header, link));
                //    }
                //}

                //foreach (var item in team)
                //{
                //    if (lang.Equals("tr")) route = "ekibimiz"; else route = "ourteam";
                //    link = "/" + lang + "/" + route;
                //    result.Add(Tuple.Create(route, link));
                //    break;
                //}
                return result;
            }
        }
    }
}
