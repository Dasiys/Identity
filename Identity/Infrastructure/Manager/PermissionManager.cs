using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using Domin.Create;

namespace Infrastructure.Manager
{
    public class PermissionManager
    {
        private readonly IdentityDbContext _dbContext = new IdentityDbContext();

        public void Add(Permission entity)
        {
            _dbContext.Set<Permission>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbContext.SaveChanges();
        }

        private IQueryable<Permission> All => _dbContext.Set<Permission>();

        public IList<PermissionDto> GetPermissionDto(Expression<Func<Permission, bool>> preExpression)
        {
            return All.Where(preExpression).Select(_ => new PermissionDto() { Name = _.Name, Id = _.Id, ParentId = _.ParentId }).ToList();
        }
    }
}
