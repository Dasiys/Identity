using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Domin.Create;

namespace Domain.Create
{
    public class RoleEditModel
    {
        public AppRole Role { get; set; }

        public IEnumerable<AppUser> Members { set; get; }

        public IEnumerable<AppUser> NonMembers { set; get; }

        public IEnumerable<PermissionShowModel> Permission { set; get; }
    }

    public class RoleModificationModel
    {
        public string RoleId { set; get; }
        public string RoleName { set; get; }
        public string[] IdsToAdd { set; get; }
        public string[] IdsToDelete { set; get; }
        public string[] PermissionIds { set; get; }

    }
}
