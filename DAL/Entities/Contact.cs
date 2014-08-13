using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Contact
    {
        public int ContactId { get; set; }
        [Display(Name="Adres")]
        [Required(ErrorMessage = "Adres Bilgisini Giriniz")]
        public string Address { get; set; }
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
        [Display(Name = "Faks")]
        public string Fax { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Bilgisini Giriniz")]
        public string Email { get; set; }
    }

    public class ContactHome
    {
        [Key]
        public int ContactId { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime TimeCreated { get; set; }

        public bool Deleted { get; set; }

    }
}
