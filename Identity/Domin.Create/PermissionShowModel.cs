using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Create
{
    /// <summary>
    /// 权限展示模型
    /// </summary>
    public class PermissionShowModel
    {
        /// <summary>
        /// 设置或获取权限Id
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// 设置或获取姓名
        /// </summary>
        public  string Name { set; get; }

        /// <summary>
        /// 设置或获取自权限
        /// </summary>
        public  IList<PermissionShowModel> ChildPermissionShowModels { set; get; }=new List<PermissionShowModel>();

        /// <summary>
        /// 设置或获取是否选中
        /// </summary>
        public bool IsChecked { set; get; }
    }
}
