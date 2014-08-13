using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class AdvancedSearchModel
    {
        public web.Controllers.SearchEstateModel search { get; set; }
        public List<Estate> estates { get; set; }

        public AdvancedSearchModel(){}

        public AdvancedSearchModel(List<Estate> estates, web.Controllers.SearchEstateModel search)
        {
            this.estates = estates;
            this.search = search;
        }
    }
}