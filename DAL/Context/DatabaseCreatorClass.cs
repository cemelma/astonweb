using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;

namespace myBLOGData.Context
{
    public class DatabaseCreatorClass : IDatabaseInitializer<MainContext>
    {
        public void InitializeDatabase(MainContext context)
        {
            if (context.Database.Exists())
            {
                if (!context.Database.CompatibleWithModel(true))
                {
                    context.Database.Delete();
                    context.Database.Create();
                }
            }
            else
            {
                context.Database.Create();
            }
        }
    }
}
