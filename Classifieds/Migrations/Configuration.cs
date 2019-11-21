namespace Classifieds.Migrations
{
    using Classifieds.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Classifieds.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            if (context.ClassifiedTypes.Count() == 0)
            {
                string[] list = { "For Sale", "Wanted", "Services", "Hiring" };
                ClassifiedType[] types = new ClassifiedType[list.Length];
                for (byte i = 0; i < list.Length; i++)
                {
                    types[i] = new ClassifiedType() { Id = i, Name = list[i] };
                }
                context.ClassifiedTypes.AddRange(types);
            }
        }
    }
}
