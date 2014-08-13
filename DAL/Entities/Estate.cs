using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Estate
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Emlak Tipi")]
        public int TypeId { get; set; }
        [Display(Name = "İşlem Tipi")]
        public int TransactionId { get; set; }

        //İl
        [Display(Name = "İl Seçimi")]
        [Required(ErrorMessage = "İli giriniz")]
        public int CountryId { get; set; }

        //İlçe
        [Display(Name = "İlçe Seçimi")]
        [Required(ErrorMessage = "İlçeyi giriniz")]
        public int TownId { get; set; }
        //Semt
        [Display(Name = "Semt Seçimi")]
        [Required(ErrorMessage = "Semti giriniz")]
        public int DistrictId { get; set; }
        [Display(Name = "Fiyat")]
        //[Required(ErrorMessage = "Fiyatı giriniz")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]

        public decimal Price { get; set; }

        public string PriceTypeId { get; set; }
        
        //Metre kare
        [Display(Name = "Metre Kare")]
         public string Size { get; set; }
        
        //Bina yaşı
        [Display(Name = "Bina Yaşı")]
        public string Age { get; set; }
       
        //Danışman
        [Display(Name = "Danışman")]
        public string Consultant { get; set; }
        
        //Oda Sayısı
        [Display(Name = "Oda Sayısı")]
        public string RoomNumber { get; set; }

        [Display(Name = "Referans Numarası")]
        //[Required(ErrorMessage = "Referans numarasını giriniz")]
        public string ReferenceNo { get; set; }

        [Display(Name = "Emlak Resmi")]
        public  string Photo { get; set; }
        //ilan açıklaması
        [Display(Name = "İlan İçeriği")]
        //[Required(ErrorMessage = "İçeriği giriniz")]
        public string Content { get; set; }
       
        //İan Adı
        [Display(Name = "İlan Başlığı")]
        [Required(ErrorMessage = "İlan başlığını giriniz")]
        public string Header { get; set; }

        [Display(Name = "Öne Çıkan Emlak")]
        public bool Popular { get; set; }

        //ziyaret edilme sayısı. öne çıkanar için belki bunu kullanırız.
        public int VisitedCount { get; set; }

        [Display(Name = "Dil Seçimi")]
        [Required(ErrorMessage = "Dili giriniz")]
        public string Language { get; set; }

        [Display(Name = "Eklenme Tarihi")]
        //[Required(ErrorMessage = "Eklenme tarihini giriniz")]
        public string TimeCreated { get; set; }

        [Display(Name = "Brüt Alan")]
        public string BrutAlan { get; set; }

        [Display(Name = "Net Alan")]
        public string NetAlan { get; set; }

        [Display(Name = "Bulunduğu Arsa m2")]
        public string BulunduguArsa { get; set; }

        [Display(Name = "BinaTabanAlani")]
        public string BinaTabanAlani { get; set; }

        [Display(Name = "Bölüm/Oda Sayısı")]
        public string BolumOdaSayisi { get; set; }

        [Display(Name = "Banyo/WC")]
        public string BanyoWc { get; set; }

        [Display(Name = "Binadaki Kat Sayısı")]
        public string BinaKatSayisi { get; set; }

        [Display(Name = "Bulunduğu Kat")]
        public string BulunduguKat { get; set; }

        [Display(Name = "CepheYon")]
        public string CepheYon { get; set; }

        [Display(Name = "Isınma Tipi")]
        public string IsınmaTipi { get; set; }

        [Display(Name = "YakitTipi")]
        public string YakitTipi { get; set; }

        [Display(Name = "Otopark")]
        public string Otopark { get; set; }

        [Display(Name = "YapininDurumu")]
        public string YapininDurumu { get; set; }

        [Display(Name = "KullanimDurumu")]
        public string KullanimDurumu { get; set; }

        [Display(Name = "TapuDurumu")]
        public string TapuDurumu { get; set; }

        [Display(Name = "Depozito")]
        public string Depozito { get; set; }

        [Display(Name = "Aidat/Yönetim")]
        public string AidatYonetim { get; set; }

        [Display(Name = "Devren")]
        public string Devren { get; set; }

        [Display(Name = "Takas")]
        public string Takas { get; set; }

        [Display(Name = "Krediye Uygunluk")]
        public string KrediDurumu { get; set; }

        [Display(Name = "Kira Getirisi")]
        public string KiraGetirisi { get; set; }

        public virtual Country Country { get; set; }
        public virtual Town Town { get; set; }
        public virtual District District { get; set; }

        [Display(Name = "Dosya")]
        public string EmlakDosyasi { get; set; }
    }
}
