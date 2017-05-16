using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Permission
    {
        /// <summary>
        /// 设置或获取Id
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// 设置或获取名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 设置或获取父级Id
        /// </summary>
        public int ParentId { set; get; }

        /// <summary>
        /// 设置或获取角色
        /// </summary>
        public virtual ICollection<AppRole> AppRoles { set; get; }
        
    }
}
