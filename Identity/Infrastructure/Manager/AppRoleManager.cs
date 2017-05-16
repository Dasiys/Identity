using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Infrastructure.Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Infrastructure.Manager
{
    public class AppRoleManager:RoleManager<AppRole>,IDisposable
    {
        public AppRoleManager(RoleStore<AppRole> store) : base(store) { }

        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options,IOwinContext context)
        {
            return new AppRoleManager(new RoleStore<AppRole>(context.Get<AppIdentityDbContext>()));
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="role"></param>
        /// <param name="perissionIds"></param>
        /// <param name="context"></param>
        public void AddRolePermission(AppRole role, string[] perissionIds,AppIdentityDbContext context)
        {
            if (role != null)
            {
                role.Permissions.Clear();
                role.Permissions =  new PermissionManager(context).GetPermission(m => perissionIds.Contains(m.Id.ToString()));
            }
        }
    }
}
