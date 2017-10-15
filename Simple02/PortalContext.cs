using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace Portal
{
    public class PortalContext : DbContext
    {
        static PortalContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PortalContext>());
        }

        //public DbSet<Province> Provinces { get; set; }
 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new ProvinceMap());
          
        }
    }
}