namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
/*
    internal sealed class Configuration : DbMigrationsConfiguration<Simple02.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }*/

    internal sealed class Configuration01 : DbMigrationsConfiguration<Simple02.Models.NewDatabase>
    {
        public Configuration01()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(NewDatabase context)
        {
            //base.Seed(context);

        }
    }
}
