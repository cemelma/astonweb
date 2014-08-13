using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.TeklifBL
{
    public class TeklifUrun_Urun
    {
        public int TeklifUrunId { get; set; }
        public int TeklifId { get; set; }
        public int UrunId { get; set; }
        public decimal Fiyat { get; set; }
        public int Adet { get; set; }
        public string Toplam { get; set; }
        public bool Donanim { get; set; }
        public decimal DonanimFiyat { get; set; }
        public string ParaBirimi { get; set; }
        public string UrunKod { get; set; }
        public string UrunResim { get; set; }
        public string UrunAdi { get; set; }
    }
}
