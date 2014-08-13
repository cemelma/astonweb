using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using System.Data;
using System.Data.Entity;
using System.Web;
using BLL.MailBL;
using System.Net.Mail;
using System.Net;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Web.Hosting;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;
namespace BLL.TeklifBL
{
    public class TeklifManager
    {
        public static List<Teklif> GetList()
        {
            using (MainContext db = new MainContext())
            {
                int status = Convert.ToInt32(EnumTeklifTip.Onaylanmadi);
                var list = db.Teklif.OrderByDescending(d => d.TeklifTarihi).ToList();
                return list;
            }
        }

        public static List<Teklif> GetList(int type)
        {
            using (MainContext db = new MainContext())
            {

                var list = db.Teklif.Where(d => d.Durum == type).OrderByDescending(d => d.TeklifTarihi).ToList();
                return list;
            }
        }

        public static Teklif GetTeklifById(int teklifid)
        {
            using (MainContext db = new MainContext())
            {

                var model = db.Teklif.SingleOrDefault(d => d.TeklifId == teklifid);
                if (model != null)
                    return model;
                else return null;
            }
        }

        public static List<TeklifUrun_Urun> GetUrunList(int teklifid)
        {
            using (MainContext db = new MainContext())
            {

                // var model = db.TeklifUrun.Where(d => d.TeklifId == teklifid).ToList();
                var model = (from t in db.TeklifUrun
                             join p in db.Product on t.UrunId equals p.ProductId
                             join tk in db.Teklif on t.TeklifId equals tk.TeklifId
                             where t.TeklifId == teklifid
                             select new
                             {
                                 t.TeklifUrunId,
                                 t.TeklifId,
                                 t.UrunId,
                                 t.Fiyat,
                                 t.Adet,
                                 t.Toplam,
                                 t.Donanim,
                                 t.DonanimFiyat,
                                 t.ParaBirimi,
                                 p.Code,
                                 p.ProductImageThumb,
                                 p.Name
                             }).ToList();

                List<TeklifUrun_Urun> liste = new List<TeklifUrun_Urun>();
                foreach (var item in model)
                {
                    TeklifUrun_Urun m = new TeklifUrun_Urun();
                    m.TeklifUrunId = item.TeklifUrunId;
                    m.TeklifId = item.TeklifId;
                    m.UrunId = item.UrunId;
                    m.Fiyat = item.Fiyat;
                    m.Adet = item.Adet;
                    m.Toplam = item.Toplam;
                    m.Donanim = item.Donanim;
                    m.DonanimFiyat = item.DonanimFiyat;
                    m.ParaBirimi = item.ParaBirimi;
                    m.UrunKod = item.Code;
                    m.UrunResim = item.ProductImageThumb;
                    m.UrunAdi = item.Name;
                    liste.Add(m);
                }

                return liste;
            }
        }

        public static bool UpdateTeklif(Teklif teklif)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    var tek = db.Teklif.Where(d => d.TeklifId == teklif.TeklifId).FirstOrDefault();
                    if (tek != null)
                    {
                        tek.Kurum = teklif.Kurum;
                        tek.Unvan = teklif.Unvan;
                        tek.Adsoyad = teklif.Adsoyad;
                        tek.Gsm = teklif.Gsm;
                        tek.Durum = teklif.Durum;
                        tek.Eposta = teklif.Eposta;
                        tek.Not = teklif.Not;
                        tek.CevapTarihi = teklif.CevapTarihi;
                        tek.Tel = teklif.Tel;
                        tek.Fax = teklif.Fax;
                        tek.CevapTarihi = teklif.CevapTarihi;
                        tek.TeklifNo = teklif.TeklifNo;
                        tek.GecerlilikSuresi = teklif.GecerlilikSuresi;
                        tek.TeslimatSuresi = teklif.TeslimatSuresi;
                        db.SaveChanges();
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static bool AddTeklif(Teklif teklif, TeklifUrun[] teklifurun, Dictionary<string, string>[] products)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    teklif.Durum = (int)EnumTeklifTip.Onaylanmadi;
                    teklif.TeklifTarihi = DateTime.Now;
                    teklif.ParaBirimi = "TL";
                    teklif.GecerlilikSuresi = 90;
                    teklif.TeslimatSuresi = "90";
                    db.Teklif.Add(teklif);
                    db.SaveChanges();

                    int[] plist = new int[50];

                    int k = 0;

                    foreach (var element in products)
                    {
                        foreach (var entry in element)
                        {
                            plist[k] = Convert.ToInt32(entry.Value);
                        }
                        k++;
                    }

                    decimal toplamTutar = 0;
                    for (int i = 0; i < teklifurun.Count(); i++)
                    {
                        int pid = Convert.ToInt32(plist[i]);
                        var prod = db.Product.Where(a => a.ProductId == pid).SingleOrDefault();

                        teklifurun[i].TeklifId = teklif.TeklifId;
                        teklifurun[i].UrunId = plist[i];
                        teklifurun[i].Fiyat = prod.Price;
                        if (teklifurun[i].Donanim && prod.Hardware)
                        {
                            teklifurun[i].Toplam = (Convert.ToDouble((prod.Price + prod.HardwarePrice) * teklifurun[i].Adet)).ToString();
                            teklifurun[i].DonanimFiyat = (decimal)prod.HardwarePrice;
                        }
                        else
                        {
                            teklifurun[i].Toplam = (Convert.ToDouble(prod.Price * teklifurun[i].Adet)).ToString();
                        }
                        toplamTutar += Convert.ToDecimal(teklifurun[i].Toplam);
                        db.TeklifUrun.Add(teklifurun[i]);
                    }

                    teklif.FaturaTutar = Convert.ToDecimal(toplamTutar);
                    teklif.KDV = Convert.ToDecimal(Convert.ToDouble(teklif.FaturaTutar) * 0.18);

                    db.SaveChanges();
                    var mset = MailManager.GetMailSettings();
                    var msend = MailManager.GetMailUsersList(1);

                    using (var client = new SmtpClient(mset.ServerHost, mset.Port))
                    {
                        client.EnableSsl = false;
                        client.Credentials = new NetworkCredential(mset.ServerMail, mset.Password);
                        var mail = new MailMessage();
                        mail.From = new MailAddress(mset.ServerMail);
                        foreach (var item in msend)
                            mail.To.Add(item.MailAddress);
                        mail.Subject = "Teklif Talebi";
                        mail.IsBodyHtml = true;
                        mail.Body = "<p>" + "Teklif detaylarına aşağıdaki linkten ulaşabilirsiniz: " + "</p>";
                        mail.Body += "<a href='http://www.web.com/yonetim'>http://www.web.com/yonetim</a>";

                        if (mail.To.Count > 0) client.Send(mail);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }


        public static string[] HesaplamaYap(int id, string fiyat, int adet, string donanim, int teklifid)
        {
            using (MainContext db = new MainContext())
            {
                var urun = db.TeklifUrun.Where(d => d.TeklifId == teklifid && d.UrunId == id).SingleOrDefault();
                urun.Fiyat = Convert.ToDecimal(fiyat);
                urun.Adet = adet;
                if (!string.IsNullOrEmpty(donanim))
                {
                    urun.DonanimFiyat = Convert.ToDecimal(donanim);
                    urun.Toplam = (Convert.ToDecimal(fiyat) * adet + Convert.ToDecimal(donanim)*adet).ToString();
                }
                 
                else urun.Toplam = (Convert.ToDecimal(fiyat) * adet).ToString();

                db.SaveChanges();


                var urunler = db.TeklifUrun.Where(d => d.TeklifId == teklifid).ToList();
                decimal fatura = 0;
                foreach (var item in urunler)
                {
                    fatura += Convert.ToDecimal(item.Toplam);
                }

                var teklif = db.Teklif.Where(d => d.TeklifId == teklifid).SingleOrDefault();
                teklif.FaturaTutar = fatura;
                teklif.KDV = fatura * 18 / 100;
                db.SaveChanges();

                string[] returnvalues = new string[3];
                returnvalues[0] = teklif.FaturaTutar.ToString();
                returnvalues[1] = ";";
                returnvalues[2] = teklif.KDV.ToString();
                return returnvalues;
            }
        }

        public static MemoryStream ProformaOnizle(string tekid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    int rowcount = 0;

                    var teklif = TeklifManager.GetTeklifById(Convert.ToInt32(tekid));
                    var teklifurun = TeklifManager.GetUrunList(Convert.ToInt32(tekid));
                    BaseFont arial = BaseFont.CreateFont("C:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Font font = new Font(arial, 8, Font.NORMAL, Color.DARK_GRAY);
                    Font boldfont = new Font(arial, 8, Font.BOLD, Color.DARK_GRAY);
                    Font ortafont = new Font(arial, 10, Font.NORMAL, Color.DARK_GRAY);
                    Font header = new Font(arial, 12, Font.BOLD); 
                    var document = new iTextSharp.text.Document(PageSize.A4, 37, 30, 25, 25);
                    var output = new MemoryStream();
                    var writer = PdfWriter.GetInstance(document, output);
                    writer.SetPdfVersion(PdfWriter.PDF_VERSION_1_7);
                    writer.CompressionLevel = PdfStream.NO_COMPRESSION;
                    document.Open();
                    PdfPTable table = new PdfPTable(2);
                    table.TotalWidth = 516f;
                    table.LockedWidth = true;
                    float[] widths = new float[] { 7.2f, 1.8f };
                    table.SetWidths(widths);
                    table.HorizontalAlignment = 1;
                    
                    table.SpacingBefore = 0f;
                    table.SpacingAfter = 0f;
                    table.DefaultCell.Border = 0;

                    PdfPCell cell;

                    cell = new PdfPCell(new Paragraph(""));
                    cell.Padding = 0;
                    cell.FixedHeight = 160f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(teklif.Kurum,font));
                    cell.Padding = 0;
                    cell.PaddingLeft = 2f;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(DateTime.Now.ToShortDateString(), font));
                    cell.Padding = 0;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);
                    
                    string unvanadsoyad = string.Empty;

                    if (string.IsNullOrEmpty(teklif.Unvan))
                        unvanadsoyad = teklif.Adsoyad;
                    else
                        unvanadsoyad = teklif.Unvan + " " + teklif.Adsoyad;

                    cell = new PdfPCell(new Paragraph(unvanadsoyad, font));
                    cell.Padding = 0;
                    cell.PaddingLeft = 2f;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(teklif.TeklifNo, font));
                    cell.Padding = 0;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(""));
                    cell.Padding = 0;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("BEKLİYOR", font));
                    cell.Padding = 0;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(""));
                    cell.Padding = 0;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(teklif.GecerlilikSuresi.ToString() + " GÜN", boldfont));
                    cell.Padding = 0;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(teklif.Gsm, font));
                    cell.Padding = 0;
                    cell.PaddingLeft = 36f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(teklif.TeslimatSuresi + " İŞ GÜNÜ", boldfont));
                    cell.Padding = 0;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    document.Add(table);
                    ///////////////////////////////////////////////////////////////////////////

                    table = new PdfPTable(2);
                    table.TotalWidth = 400f;
                    table.LockedWidth = true;
                    widths = new float[] { 1.6f, 2.4f };
                    table.SetWidths(widths);
                    table.HorizontalAlignment = Element.ALIGN_LEFT;

                    table.SpacingBefore = 0f;
                    table.SpacingAfter = 0f;
                    table.DefaultCell.Border = 0;

                    cell = new PdfPCell(new Paragraph(teklif.Tel, font));
                    cell.Padding = 0;
                    cell.PaddingLeft = 42f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(teklif.Fax, font));
                    cell.Padding = 0;
                    cell.PaddingLeft = 14f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);
                    
                    document.Add(table);
                    ////////////////////////////////////////////////////////////////////////////

                    table = new PdfPTable(6);
                    table.TotalWidth = 522f;
                    table.LockedWidth = true;
                    widths = new float[] { 0.8f, 3.9f, 0.7f, 0.7f, 1f, 1f };
                    table.SetWidths(widths);
                    table.HorizontalAlignment = Element.ALIGN_LEFT;

                    cell = new PdfPCell(new Paragraph(""));
                    cell.Padding = 0;
                    cell.FixedHeight = 15f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);
                    table.AddCell(cell);
                    table.AddCell(cell);
                    table.AddCell(cell);
                    table.AddCell(cell);
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Stok Kodu", boldfont));
                    cell.BackgroundColor = Color.LIGHT_GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.FixedHeight = 13f;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.LIGHT_GRAY.Darker();
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Stok Adı", boldfont));
                    cell.BackgroundColor = Color.LIGHT_GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.FixedHeight = 13f;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.LIGHT_GRAY.Darker();
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Birimi", boldfont));
                    cell.BackgroundColor = Color.LIGHT_GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.FixedHeight = 13f;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.LIGHT_GRAY.Darker();
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Miktar", boldfont));
                    cell.BackgroundColor = Color.LIGHT_GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.FixedHeight = 13f;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.LIGHT_GRAY.Darker();
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Birim Fiyat", boldfont));
                    cell.BackgroundColor = Color.LIGHT_GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.FixedHeight = 13f;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.LIGHT_GRAY.Darker();
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Tutarı", boldfont));
                    cell.BackgroundColor = Color.LIGHT_GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.FixedHeight = 13f;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.LIGHT_GRAY.Darker();
                    table.AddCell(cell);

                    ////////////////////////////////////////////////////////////////////////////

                    foreach (var item in teklifurun)
                    {
                        rowcount++;

                        cell = new PdfPCell(new Paragraph(item.UrunKod, font));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.FixedHeight = 13f;
                        cell.BorderWidth = 1;
                        cell.BorderColor = Color.LIGHT_GRAY.Darker();
                        table.AddCell(cell);

                        cell = new PdfPCell(new Paragraph(item.UrunAdi, font));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.FixedHeight = 13f;
                        cell.BorderWidth = 1;
                        cell.BorderColor = Color.LIGHT_GRAY.Darker();
                        table.AddCell(cell);

                        cell = new PdfPCell(new Paragraph("ADET", font));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.FixedHeight = 13f;
                        cell.BorderWidth = 1;
                        cell.BorderColor = Color.LIGHT_GRAY.Darker();
                        table.AddCell(cell);

                        cell = new PdfPCell(new Paragraph(item.Adet.ToString(), font));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.FixedHeight = 13f;
                        cell.BorderWidth = 1;
                        cell.BorderColor = Color.LIGHT_GRAY.Darker();
                        table.AddCell(cell);

                        cell = new PdfPCell(new Paragraph(item.Fiyat.ToString() + "TL", font));
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell.FixedHeight = 13f;
                        cell.BorderWidth = 1;
                        cell.BorderColor = Color.LIGHT_GRAY.Darker();
                        table.AddCell(cell);

                        string tutar = (item.Fiyat * Convert.ToDecimal(item.Adet)).ToString();

                        cell = new PdfPCell(new Paragraph(tutar + "TL", font));
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell.FixedHeight = 13f;
                        cell.BorderWidth = 1;
                        cell.BorderColor = Color.LIGHT_GRAY.Darker();
                        table.AddCell(cell);

                        if (item.Donanim)
                        {
                            rowcount++;
                            cell = new PdfPCell(new Paragraph(item.UrunKod, font));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.FixedHeight = 13f;
                            cell.BorderWidth = 1;
                            cell.BorderColor = Color.LIGHT_GRAY.Darker();
                            table.AddCell(cell);

                            cell = new PdfPCell(new Paragraph("YAZILIM, DONANIM, PC BAĞLANTISI", font));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.FixedHeight = 13f;
                            cell.BorderWidth = 1;
                            cell.BorderColor = Color.LIGHT_GRAY.Darker();
                            table.AddCell(cell);

                            cell = new PdfPCell(new Paragraph("ADET", font));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.FixedHeight = 13f;
                            cell.BorderWidth = 1;
                            cell.BorderColor = Color.LIGHT_GRAY.Darker();
                            table.AddCell(cell);

                            cell = new PdfPCell(new Paragraph(item.Adet.ToString(), font));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.FixedHeight = 13f;
                            cell.BorderWidth = 1;
                            cell.BorderColor = Color.LIGHT_GRAY.Darker();
                            table.AddCell(cell);

                            cell = new PdfPCell(new Paragraph(item.DonanimFiyat.ToString() + "TL", font));
                            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            cell.FixedHeight = 13f;
                            cell.BorderWidth = 1;
                            cell.BorderColor = Color.LIGHT_GRAY.Darker();
                            table.AddCell(cell);

                            string donanimtutar = (item.DonanimFiyat * Convert.ToDecimal(item.Adet)).ToString();

                            cell = new PdfPCell(new Paragraph(donanimtutar + "TL", font));
                            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            cell.FixedHeight = 13f;
                            cell.BorderWidth = 1;
                            cell.BorderColor = Color.LIGHT_GRAY.Darker();
                            table.AddCell(cell);
                        }
                    }

                    document.Add(table);

                    ////////////////////////////////////////////////////////////////////////////

                    table = new PdfPTable(3);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 3.6f, 1.4f, 0.9f };
                    table.SetWidths(widths);
                    table.HorizontalAlignment = Element.ALIGN_LEFT;

                    cell = new PdfPCell(new Paragraph(""));
                    cell.Padding = 0;
                    cell.FixedHeight = 222f - (13f * rowcount);
                    cell.BorderWidth = 0;
                    table.AddCell(cell);
                    table.AddCell(cell);
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(teklif.Not, ortafont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.FixedHeight = 12f;
                    cell.BorderWidth = 0;
                    cell.PaddingLeft = 15f;
                    cell.Rowspan = 4;
                    cell.BorderColor = Color.LIGHT_GRAY.Darker();
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(""));
                    cell.FixedHeight = 12f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    string kdvHaric = (teklif.FaturaTutar).ToString() + " TL";

                    cell = new PdfPCell(new Paragraph(kdvHaric, font));
                    cell.Padding = 0;
                    cell.FixedHeight = 12f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(""));
                    cell.Padding = 0;
                    cell.FixedHeight = 12f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(kdvHaric, font));
                    cell.Padding = 0;
                    cell.FixedHeight = 12f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(""));
                    cell.Padding = 0;
                    cell.FixedHeight = 12f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(teklif.KDV.ToString() + " TL", font));
                    cell.Padding = 0;
                    cell.FixedHeight = 12f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(""));
                    cell.Padding = 0;
                    cell.FixedHeight = 12f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph((teklif.FaturaTutar + teklif.KDV).ToString() + " TL", font));
                    cell.Padding = 0;
                    cell.FixedHeight = 12f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);


                    document.Add(table);





                    var logo = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath("~/Content/images/proforma/top.tif"));
                    logo.ScalePercent(70f);

                    logo.SetAbsolutePosition(20, 35);

                    document.Add(logo);

                    teklif.Durum = (int)EnumTeklifTip.Onaylandi;

                    db.SaveChanges();

                    writer.CloseStream = false;
                    document.Close();

                    var mset = MailManager.GetMailSettings();

                    using (var client = new SmtpClient(mset.ServerHost, mset.Port))
                    {
                        client.EnableSsl = false;
                        client.Credentials = new NetworkCredential(mset.ServerMail, mset.Password);
                        var mail = new MailMessage();
                        mail.From = new MailAddress(mset.ServerMail);
                        mail.To.Add(teklif.Eposta);
                        mail.Subject = "Deneysan - Proforma Faturası";
                        mail.Body = "Proforma faturanız ekte bulunmaktadır.";
                        if (document != null)
                        {
                            output.Position = 0;
                            var attachment = new Attachment(output, "proforma-fatura.pdf");
                            mail.Attachments.Add(attachment);
                        }
                        // client.Send(mail);
                    }

                    return output;

                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static bool ProformaGonder(string tekid)
        {
            using (MainContext db = new MainContext())
            {
                try
                {
                    int rowcount = 0;

                    var teklif = TeklifManager.GetTeklifById(Convert.ToInt32(tekid));
                    var teklifurun = TeklifManager.GetUrunList(Convert.ToInt32(tekid));
                    BaseFont arial = BaseFont.CreateFont("C:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Font font = new Font(arial, 8, Font.NORMAL, Color.DARK_GRAY);
                    Font boldfont = new Font(arial, 8, Font.BOLD, Color.DARK_GRAY);
                    Font ortafont = new Font(arial, 10, Font.NORMAL, Color.DARK_GRAY);
                    Font header = new Font(arial, 12, Font.BOLD);
                    var document = new iTextSharp.text.Document(PageSize.A4, 37, 30, 25, 25);
                    var output = new MemoryStream();
                    var writer = PdfWriter.GetInstance(document, output);
                    writer.SetPdfVersion(PdfWriter.PDF_VERSION_1_7);
                    writer.CompressionLevel = PdfStream.NO_COMPRESSION;
                    document.Open();
                    PdfPTable table = new PdfPTable(2);
                    table.TotalWidth = 516f;
                    table.LockedWidth = true;
                    float[] widths = new float[] { 7.2f, 1.8f };
                    table.SetWidths(widths);
                    table.HorizontalAlignment = 1;

                    table.SpacingBefore = 0f;
                    table.SpacingAfter = 0f;
                    table.DefaultCell.Border = 0;

                    PdfPCell cell;

                    cell = new PdfPCell(new Paragraph(""));
                    cell.Padding = 0;
                    cell.FixedHeight = 160f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(teklif.Kurum, font));
                    cell.Padding = 0;
                    cell.PaddingLeft = 2f;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(DateTime.Now.ToShortDateString(), font));
                    cell.Padding = 0;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    string unvanadsoyad = string.Empty;

                    if (string.IsNullOrEmpty(teklif.Unvan))
                        unvanadsoyad = teklif.Adsoyad;
                    else
                        unvanadsoyad = teklif.Unvan + " " + teklif.Adsoyad;

                    cell = new PdfPCell(new Paragraph(unvanadsoyad, font));
                    cell.Padding = 0;
                    cell.PaddingLeft = 2f;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(teklif.TeklifNo, font));
                    cell.Padding = 0;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(""));
                    cell.Padding = 0;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("BEKLİYOR", font));
                    cell.Padding = 0;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(""));
                    cell.Padding = 0;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(teklif.GecerlilikSuresi.ToString() + " GÜN", boldfont));
                    cell.Padding = 0;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(teklif.Gsm, font));
                    cell.Padding = 0;
                    cell.PaddingLeft = 36f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(teklif.TeslimatSuresi + " İŞ GÜNÜ", boldfont));
                    cell.Padding = 0;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    document.Add(table);
                    ///////////////////////////////////////////////////////////////////////////

                    table = new PdfPTable(2);
                    table.TotalWidth = 400f;
                    table.LockedWidth = true;
                    widths = new float[] { 1.6f, 2.4f };
                    table.SetWidths(widths);
                    table.HorizontalAlignment = Element.ALIGN_LEFT;

                    table.SpacingBefore = 0f;
                    table.SpacingAfter = 0f;
                    table.DefaultCell.Border = 0;

                    cell = new PdfPCell(new Paragraph(teklif.Tel, font));
                    cell.Padding = 0;
                    cell.PaddingLeft = 42f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(teklif.Fax, font));
                    cell.Padding = 0;
                    cell.PaddingLeft = 14f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.FixedHeight = 9f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    document.Add(table);
                    ////////////////////////////////////////////////////////////////////////////

                    table = new PdfPTable(6);
                    table.TotalWidth = 522f;
                    table.LockedWidth = true;
                    widths = new float[] { 0.8f, 3.9f, 0.7f, 0.7f, 1f, 1f };
                    table.SetWidths(widths);
                    table.HorizontalAlignment = Element.ALIGN_LEFT;

                    cell = new PdfPCell(new Paragraph(""));
                    cell.Padding = 0;
                    cell.FixedHeight = 15f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);
                    table.AddCell(cell);
                    table.AddCell(cell);
                    table.AddCell(cell);
                    table.AddCell(cell);
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Stok Kodu", boldfont));
                    cell.BackgroundColor = Color.LIGHT_GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.FixedHeight = 13f;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.LIGHT_GRAY.Darker();
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Stok Adı", boldfont));
                    cell.BackgroundColor = Color.LIGHT_GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.FixedHeight = 13f;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.LIGHT_GRAY.Darker();
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Birimi", boldfont));
                    cell.BackgroundColor = Color.LIGHT_GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.FixedHeight = 13f;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.LIGHT_GRAY.Darker();
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Miktar", boldfont));
                    cell.BackgroundColor = Color.LIGHT_GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.FixedHeight = 13f;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.LIGHT_GRAY.Darker();
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Birim Fiyat", boldfont));
                    cell.BackgroundColor = Color.LIGHT_GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.FixedHeight = 13f;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.LIGHT_GRAY.Darker();
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Tutarı", boldfont));
                    cell.BackgroundColor = Color.LIGHT_GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.FixedHeight = 13f;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.LIGHT_GRAY.Darker();
                    table.AddCell(cell);

                    ////////////////////////////////////////////////////////////////////////////

                    foreach (var item in teklifurun)
                    {
                        rowcount++;

                        cell = new PdfPCell(new Paragraph(item.UrunKod, font));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.FixedHeight = 13f;
                        cell.BorderWidth = 1;
                        cell.BorderColor = Color.LIGHT_GRAY.Darker();
                        table.AddCell(cell);

                        cell = new PdfPCell(new Paragraph(item.UrunAdi, font));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.FixedHeight = 13f;
                        cell.BorderWidth = 1;
                        cell.BorderColor = Color.LIGHT_GRAY.Darker();
                        table.AddCell(cell);

                        cell = new PdfPCell(new Paragraph("ADET", font));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.FixedHeight = 13f;
                        cell.BorderWidth = 1;
                        cell.BorderColor = Color.LIGHT_GRAY.Darker();
                        table.AddCell(cell);

                        cell = new PdfPCell(new Paragraph(item.Adet.ToString(), font));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.FixedHeight = 13f;
                        cell.BorderWidth = 1;
                        cell.BorderColor = Color.LIGHT_GRAY.Darker();
                        table.AddCell(cell);

                        cell = new PdfPCell(new Paragraph(item.Fiyat.ToString() + "TL", font));
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell.FixedHeight = 13f;
                        cell.BorderWidth = 1;
                        cell.BorderColor = Color.LIGHT_GRAY.Darker();
                        table.AddCell(cell);

                        string tutar = (item.Fiyat * Convert.ToDecimal(item.Adet)).ToString();

                        cell = new PdfPCell(new Paragraph(tutar + "TL", font));
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell.FixedHeight = 13f;
                        cell.BorderWidth = 1;
                        cell.BorderColor = Color.LIGHT_GRAY.Darker();
                        table.AddCell(cell);

                        if (item.Donanim)
                        {
                            rowcount++;
                            cell = new PdfPCell(new Paragraph(item.UrunKod, font));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.FixedHeight = 13f;
                            cell.BorderWidth = 1;
                            cell.BorderColor = Color.LIGHT_GRAY.Darker();
                            table.AddCell(cell);

                            cell = new PdfPCell(new Paragraph("YAZILIM, DONANIM, PC BAĞLANTISI", font));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.FixedHeight = 13f;
                            cell.BorderWidth = 1;
                            cell.BorderColor = Color.LIGHT_GRAY.Darker();
                            table.AddCell(cell);

                            cell = new PdfPCell(new Paragraph("ADET", font));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.FixedHeight = 13f;
                            cell.BorderWidth = 1;
                            cell.BorderColor = Color.LIGHT_GRAY.Darker();
                            table.AddCell(cell);

                            cell = new PdfPCell(new Paragraph(item.Adet.ToString(), font));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.FixedHeight = 13f;
                            cell.BorderWidth = 1;
                            cell.BorderColor = Color.LIGHT_GRAY.Darker();
                            table.AddCell(cell);

                            cell = new PdfPCell(new Paragraph(item.DonanimFiyat.ToString() + "TL", font));
                            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            cell.FixedHeight = 13f;
                            cell.BorderWidth = 1;
                            cell.BorderColor = Color.LIGHT_GRAY.Darker();
                            table.AddCell(cell);

                            string donanimtutar = (item.DonanimFiyat * Convert.ToDecimal(item.Adet)).ToString();

                            cell = new PdfPCell(new Paragraph(donanimtutar + "TL", font));
                            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            cell.FixedHeight = 13f;
                            cell.BorderWidth = 1;
                            cell.BorderColor = Color.LIGHT_GRAY.Darker();
                            table.AddCell(cell);
                        }
                    }

                    document.Add(table);

                    ////////////////////////////////////////////////////////////////////////////

                    table = new PdfPTable(3);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 3.6f, 1.4f, 0.9f };
                    table.SetWidths(widths);
                    table.HorizontalAlignment = Element.ALIGN_LEFT;

                    cell = new PdfPCell(new Paragraph(""));
                    cell.Padding = 0;
                    cell.FixedHeight = 222f - (13f * rowcount);
                    cell.BorderWidth = 0;
                    table.AddCell(cell);
                    table.AddCell(cell);
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(teklif.Not, ortafont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.FixedHeight = 12f;
                    cell.BorderWidth = 0;
                    cell.PaddingLeft = 15f;
                    cell.Rowspan = 4;
                    cell.BorderColor = Color.LIGHT_GRAY.Darker();
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(""));
                    cell.FixedHeight = 12f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    string kdvHaric = (teklif.FaturaTutar).ToString() + " TL";

                    cell = new PdfPCell(new Paragraph(kdvHaric, font));
                    cell.Padding = 0;
                    cell.FixedHeight = 12f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(""));
                    cell.Padding = 0;
                    cell.FixedHeight = 12f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(kdvHaric, font));
                    cell.Padding = 0;
                    cell.FixedHeight = 12f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(""));
                    cell.Padding = 0;
                    cell.FixedHeight = 12f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(teklif.KDV.ToString() + " TL", font));
                    cell.Padding = 0;
                    cell.FixedHeight = 12f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(""));
                    cell.Padding = 0;
                    cell.FixedHeight = 12f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph((teklif.FaturaTutar + teklif.KDV).ToString() + " TL", font));
                    cell.Padding = 0;
                    cell.FixedHeight = 12f;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);


                    document.Add(table);





                    var logo = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath("~/Content/images/proforma/top.tif"));
                    logo.ScalePercent(70f);

                    logo.SetAbsolutePosition(20, 35);

                    document.Add(logo);

                    //teklif.Durum = (int)EnumTeklifTip.Onaylandi;
                    int tempTeklifID = Convert.ToInt32(tekid);
                    db.Teklif.FirstOrDefault(d => d.TeklifId == tempTeklifID).Durum = (int)EnumTeklifTip.Onaylandi;

                    db.SaveChanges();

                    writer.CloseStream = false;
                    document.Close();

                    var mset = MailManager.GetMailSettings();

                    using (var client = new SmtpClient(mset.ServerHost, mset.Port))
                    {
                        client.EnableSsl = false;
                        client.Credentials = new NetworkCredential(mset.ServerMail, mset.Password);
                        var mail = new MailMessage();
                        mail.From = new MailAddress(mset.ServerMail);
                        mail.To.Add(teklif.Eposta);
                        mail.Subject = "Deneysan - Proforma Faturası";
                        mail.Body = "Proforma faturanız ekte bulunmaktadır.";
                        if (document != null)
                        {
                            output.Position = 0;
                            var attachment = new Attachment(output, "proforma-fatura.pdf");
                            mail.Attachments.Add(attachment);
                        }
                        client.Send(mail);
                    }

                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static string TurkceKarakter(string text)
        {
            text = text.Replace("İ", "\u0130");
            text = text.Replace("ı", "\u0131");
            text = text.Replace("Ş", "\u015e");
            text = text.Replace("ş", "\u015f");
            text = text.Replace("Ğ", "\u011e");
            text = text.Replace("ğ", "\u011f");
            text = text.Replace("Ö", "\u00d6");
            text = text.Replace("ö", "\u00f6");
            text = text.Replace("ç", "\u00e7");
            text = text.Replace("Ç", "\u00c7");
            text = text.Replace("ü", "\u00fc");
            text = text.Replace("Ü", "\u00dc");
            return text;
        }

        private static StyleSheet GetStyles()
        {
            StyleSheet styles = new StyleSheet();

            string[] elements = { "html", "body", "div", "span", "dsds", "tbody", "tfoot", "thead", "tr", "th", "td" };

            foreach (var item in elements)
            {
                styles.LoadTagStyle(item, "margin", "0");
                styles.LoadTagStyle(item, "padding", "0");
                styles.LoadTagStyle(item, "border", "0");
                styles.LoadTagStyle(item, "font-size", "100%");
                styles.LoadTagStyle(item, "font", "inherit");
                styles.LoadTagStyle(item, "vertical-align", "baseline");
            }

            styles.LoadTagStyle("body", "line-height", "1");
            styles.LoadTagStyle("table", "border-collapse", "collapse");
            styles.LoadTagStyle("table", "border-spacing", "0");


            styles.LoadStyle("tnone", "border-top", "none !important");
            styles.LoadStyle("bnone", "border-bottom", "none !important");
            styles.LoadStyle("pbnone", "padding-bottom", "2px !important");
            styles.LoadStyle("ptnone", "padding-top", "2px !important");

            styles.LoadStyle("sagust", "float", "right");
            styles.LoadStyle("sagust", "background-color", "#ccc");
            styles.LoadStyle("sagust", "border", "1px solid #666");

            return new StyleSheet();
        }

        public static void DeleteTeklif(int id)
        {
            using (MainContext db = new MainContext())
            {
                var model = db.Teklif.SingleOrDefault(d => d.TeklifId == id);
                if (model != null)
                {
                    db.Teklif.Remove(model);
                    db.SaveChanges();
                }
            }
        }

        public static bool DeleteTeklifUrun(int id)
        {
            using (MainContext db = new MainContext())
            {
                var model = db.TeklifUrun.SingleOrDefault(d => d.TeklifUrunId == id);
                if (model != null)
                {
                    db.TeklifUrun.Remove(model);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
