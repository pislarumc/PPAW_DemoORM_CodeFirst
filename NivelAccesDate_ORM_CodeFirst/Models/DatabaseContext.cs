using NivelAccesDate_ORM_CodeFirst.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelAccesDate_ORM_CodeFirst.Models
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext() : base("DBConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, NivelAccesDate_ORM_CodeFirst.Migrations.Configuration>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Effect> Effects { get; set; }

        /*
         //De verificat
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOptional(a => a.User)
                .WithOptionalDependent()
                .WillCascadeOnDelete(true);
        }
        */
    }
}
