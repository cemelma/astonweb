using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class UserModel
    {
            public User NewUser { get; set; }

            public loginModel Login { get; set; }

            [Required(ErrorMessage = "Lütfen Şifrenizi Tekrar Giriniz")]
            [Display(Name = "Şifre(Tekrar):")]
            public string RePassword { get; set; }

    }


    public class loginModel
    {
        [Required(ErrorMessage = "Lütfen Emailinizi Giriniz")]
        [Display(Name = "E-Posta Adresi:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz")]
        [Display(Name = "Şifre:")]
        public string Password { get; set; }
    }

}