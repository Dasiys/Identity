﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Domin.Create
{
    public class RoleEditModel
    {
        public AppRole Role { get; set; }

        public IEnumerable<AppUser> Members { set; get; }

        public IEnumerable<AppUser> NonMembers { set; get; }
    }

    public class RoleModificationModel
    {
        public string RoleName { set; get; }
        public string[] IdsToAdd { set; get; }
        public string[] IdsToDelete { set; get; }
    }
}
