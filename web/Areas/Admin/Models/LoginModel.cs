using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Lütfen Kullanıcı Adınızı Girin.")]
        [Display(Name = "Kullanıcı E-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lütfen Şifrenizi Girin.")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}