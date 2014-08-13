using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class MailSetting
    {
        [Key]
        public int MailSettingId { get; set; }
        [Display(Name = "Server Mail Adresi")]
        [Required(ErrorMessage = "Server Mail Adresini Giriniz.")]
        [EmailAddress(ErrorMessage = "Mail Adresi Doğru Formatta Değil.")]
        public string ServerMail{ get; set; }
       
        [Display(Name = "SMTP Host Adresi")]
        [Required(ErrorMessage = "Host Adresini Giriniz.")]
        public string ServerHost { get; set; }
        
        [Display(Name = "Port Numarası")]
        [Required(ErrorMessage = "Port Numarasını Giriniz.")]
        public int Port { get; set; }
       
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Display(Name = "Security")]
        public bool Security { get; set; }
    }
}
