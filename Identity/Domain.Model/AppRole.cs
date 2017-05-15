using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain.Model
{
    public class AppRole : IdentityRole
    {
        public AppRole():base(){}

        public AppRole(string name) : base(name) { }

        /// <summary>
        /// 设置或获取权限
        /// </summary>
        public ICollection<Permission> Permissions { set; get; }
    }
}
