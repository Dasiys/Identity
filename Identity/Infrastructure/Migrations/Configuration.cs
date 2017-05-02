namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Domain.Model;
    using Infrastructure.Manager;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.Database.AppIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Infrastructure.Database.AppIdentityDbContext context)
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
            foreach (AppUser dbUser in userManager.Users)
            {
                dbUser.City = Cities.PARIS;
                dbUser.Country = Countries.FRANCE;
            }
            context.SaveChanges();
        }
    }
}
