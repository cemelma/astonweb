using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Teklif
    {
        public int TeklifId { get; set; }
        public string Kurum { get; set; }
        [DisplayName("Ünvan")]
        public string Unvan { get; set; }
        [DisplayName("Ad Soyad")]
        public string Adsoyad { get; set; }
        public string Gsm { get; set; }
        public string Tel { get; set; }
        public string Eposta { get; set; }
        public string Fax { get; set; }
        public decimal KDV { get; set; }
        [DisplayName("Teslimat Süresi")]
        public string TeslimatSuresi { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Teklif Tarihi")]
        public Nullable<DateTime> TeklifTarihi { get; set; }
        [DisplayName("Cevap Tarihi")]
        public Nullable<DateTime> CevapTarihi { get; set; }
        [DisplayName("Teklif No")]
        public string TeklifNo { get; set; }
        public string Not { get; set; }
        [DisplayName("Geçerlilik Süresi")]
        public int GecerlilikSuresi { get; set; }
        public int Durum { get; set; }
        [DisplayName("Fatura Tutarı")]
        public decimal FaturaTutar { get; set; }
        [DisplayName("Para Birimi")]
        public string ParaBirimi { get; set; }
    
    }

}
