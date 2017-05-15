using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Infrastructure.Manager;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infrastructure.Database
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext() : base("IdentityDb")
        {

        }
        static AppIdentityDbContext()
        {
            System.Data.Entity.Database.SetInitializer(new IdentityDbInit());
        }
        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PermissionMap());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Boy> Boy { set; get; }

        public DbSet<Permission> Permissions { set; get; }
    }

    public class IdentityDbInit : NullDatabaseInitializer<AppIdentityDbContext>
    {
    }
}
