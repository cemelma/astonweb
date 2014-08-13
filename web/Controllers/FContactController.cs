using BLL.ContactBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using BLL.MailBL;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using DAL.Entities;
using web.Models;
using DAL.Context;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace web.Controllers
{
    public class FContactController : Controller
    {
        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            //generate new question 
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);

            //store answer 
            Session["Captcha" + prefix] = a + b;

            //image stream 
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise 
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);
                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //add question 
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);

                //render as Jpeg 
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }
        public ActionResult Index()
        {
            ViewData["contact"] = ContactManager.GetContact();
            //SubscribeModel obj = new SubscribeModel();
            //ContactWrapperModel m = new ContactWrapperModel(contact, obj);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string namesurname, string email, string phone, string city, string subject, string body,string captcha)
        {
            try
            {
                if (Session["Captcha"] == null || Session["Captcha"].ToString() != captcha)
                {
                    TempData["captchaError"] = "Güvenlik doğrulmasını yanlış girdiniz, lütfen tekrar deneyiniz.";
                    return RedirectToAction("Index");
                }
                if (namesurname == String.Empty || email == String.Empty || subject == String.Empty || body == String.Empty)
                {
                    TempData["required"] = "true";
                    return RedirectToAction("Index");
                }

                //DB kaydet
                ContactHome contact = new ContactHome()
                {
                    FullName = namesurname, Email = email, City = city, Subject = subject, Body = body, Phone = phone
                };
                ContactManager.AddContactHome(contact);

                var mset = MailManager.GetMailSettings();
                var msend = MailManager.GetMailUsersList(0);

                using (var client = new SmtpClient(mset.ServerHost, mset.Port))
                {
                    client.EnableSsl = mset.Security;//true;//burası düzeltilecek
                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(mset.ServerMail, mset.Password);
                    
                    var mail = new MailMessage();
                    mail.From = new MailAddress(mset.ServerMail,"Zeynel Yayla");
                    //mail.Attachments.Add(new Attachment(@"C:\Sonuc.pptx"));
                    foreach (var item in msend)
                        mail.To.Add(item.MailAddress);
                    mail.Subject = subject;
                    mail.IsBodyHtml = true;
                    mail.Body = "<h3>Gönderen:" + namesurname + " - " + email + "</h3>" +
                        "<b>Tel: </b>" + phone + 
                        "<br><b>Şehir: </b>" + city +
                        "<br><h3>Mesaj:</h3><p>" + body + "</p>";
                    //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    if (mail.To.Count > 0) client.Send(mail);
                }
                TempData["sent"] = "true";
            }
            catch (Exception ex)
            {
                string message =
        "Exception type " + ex.GetType() + Environment.NewLine +
        "Exception message: " + ex.Message + Environment.NewLine +
        "Stack trace: " + ex.StackTrace + Environment.NewLine;
                if (ex.InnerException != null)
                {
                    message += "---BEGIN InnerException--- " + Environment.NewLine +
                               "Exception type " + ex.InnerException.GetType() + Environment.NewLine +
                               "Exception message: " + ex.InnerException.Message + Environment.NewLine +
                               "Stack trace: " + ex.InnerException.StackTrace + Environment.NewLine +
                               "---END Inner Exception";
                }
                TempData["exception"] = message;
                TempData["sent"] = "false";
            }

            ViewData["contact"] = ContactManager.GetContact();
            return View();
        }
    }
}
