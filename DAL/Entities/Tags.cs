using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Tags
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string Title { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string Lang { get; set; }


    }
}
