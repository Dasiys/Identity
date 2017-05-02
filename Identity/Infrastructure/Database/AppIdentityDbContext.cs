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

        public DbSet<Boy> Boy { set; get; }
    }

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppIdentityDbContext>
    {
        protected override void Seed(AppIdentityDbContext context)
        {
            PerfromInitialSetup(context);
            base.Seed(context);
        }
        public void PerfromInitialSetup(AppIdentityDbContext context)
        {
            AppUserManager userManager = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleManager = new AppRoleManager(new RoleStore<AppRole>(context));
            string roleName = "Administrators";
            string name = "Admin";
            string password = "yeweimi1234";
            string email = "710002129@qq.com";
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new AppRole(roleName));
            }
            AppUser user = userManager.FindByName(name);
            if (user == null)
            {
                userManager.Create(new AppUser
                {
                    Email = email,
                    UserName = name
                }, password);
                user = userManager.FindByName(user.UserName);
            }
            if (!userManager.IsInRole(user.Id, roleName))
            {
                userManager.AddToRole(user.Id, user.UserName);
            }
        }
    }
}
