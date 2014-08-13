using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Context
{
    public class Configration : DbMigrationsConfiguration<MainContext>
    {
        public Configration()
        {
            AutomaticMigrationsEnabled = true; // production'da false yapılacak...
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MainContext context)
        {

            base.Seed(context);
        }
    }
}
