using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Log4Net_Error
    {
        [Key]
        public int Log4Net_ErrorId { get; set; }
        
        public DateTime Date { get; set; }
        
        public String Thread { get; set; }
        public String Level { get; set; }
        public String Logger { get; set; }
        public String Message { get; set; }
        public String Exception { get; set; }
    }
}
