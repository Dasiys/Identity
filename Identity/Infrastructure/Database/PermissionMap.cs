using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Infrastructure.Database
{
    public class PermissionMap:EntityTypeConfiguration<Permission>
    {
        public PermissionMap()
        {
            HasKey(m => m.Id);
            HasMany(m => m.AppRoles)
                .WithMany(n => n.Permissions)
                .Map(n => n.MapLeftKey("PermissionId").MapRightKey("RoleId").ToTable("RolePermissionMap"));
        }
    }
}
