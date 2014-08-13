using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class AdminUser
    {
        
        public int AdminUserId { get; set; }
        public string FullName { get; set; }
       
        public string Email { get; set; }
       
        public string Password { get; set; }
    }
}
