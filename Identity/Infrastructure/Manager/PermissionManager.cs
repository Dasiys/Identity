using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Create;
using Domain.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using Domin.Create;
using Infrastructure.Database;

namespace Infrastructure.Manager
{
    public class PermissionManager
    {
        private readonly AppIdentityDbContext _dbContext ;

        public PermissionManager()
        {
           _dbContext = new AppIdentityDbContext();
        }

        public PermissionManager(AppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Permission entity)
        {
            _dbContext.Set<Permission>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbContext.SaveChanges();
        }

        public IQueryable<Permission> All => _dbContext.Set<Permission>();

        public IList<Permission> GetPermission(Expression<Func<Permission, bool>> preExpression)
        {
            return All.Where(preExpression).ToList();
        }

        /// <summary>
        /// 获取权限的展示模型
        /// </summary>
        /// <returns></returns>
        public IList<PermissionShowModel> GetShowModels()
        {
            var permissionos = GetPermission(m => true);
            return permissionos?.Where(m=>m.ParentId==0).Select(m => new PermissionShowModel()
            {
                Name = m.Name,
                Id = m.Id,
                ChildPermissionShowModels = GetChildShowModel(permissionos, m.Id)
            }).ToList();
        }

        /// <summary>
        /// 获取权限的子模型
        /// </summary>
        /// <param name="permissionDtos"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private IList<PermissionShowModel> GetChildShowModel(IList<Permission> permissionDtos,int parentId)
        {
            return permissionDtos.Where(m => m.ParentId == parentId).Select(_ => new PermissionShowModel()
            {
                Id= _.Id,
                Name = _.Name,
                ChildPermissionShowModels = GetChildShowModel(permissionDtos,_.Id)
            }).ToList();
        }

        public IList<PermissionShowModel> GetEditPermissions(List<Permission> existedModels,int paraentId=0,IList<Permission> permissions=null )
        {
            existedModels = existedModels ?? new List<Permission>();
            permissions = permissions ?? GetPermission(m => true);
            return permissions.Where(m => m.ParentId == paraentId).Select(_ => new PermissionShowModel()
            {
                Id=_.Id,
                Name =_.Name,
                IsChecked = existedModels.Any(m=>m.Id==_.Id),
                ChildPermissionShowModels = GetEditPermissions(existedModels,_.Id,permissions)
            }).ToList();
        }
    }
}
