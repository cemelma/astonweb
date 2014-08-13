using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class MailUsers
    {
        [Key]
        public int MailUserId { get; set; }
        [Display(Name="Ad Soyad")]
        public string MailUser { get; set; }
        [Display(Name = "Mail Adresi")]
        [Required(ErrorMessage="Mail Adresini Giriniz.")]
        [EmailAddress(ErrorMessage="Mail Adresi Doğru Formatta Değil.")]
        public string MailAddress { get; set; }
        [Required(ErrorMessage = "Mail Tipini Giriniz.")]
        public int MailType { get; set; }
    }
}
