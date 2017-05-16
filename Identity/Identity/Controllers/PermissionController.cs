using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Model;
using Infrastructure.Manager;

namespace Identity.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PermissionController : Controller
    {
        private readonly PermissionManager _permissionManager=new PermissionManager();
        // GET: Permission
        public ActionResult Index()
        {
            var permissions =_permissionManager.GetShowModels();
            return View(permissions);
        }

        public ActionResult AddPermission()
        {
            var permissions = _permissionManager.GetPermission(m => true)?.Select(_=>new SelectListItem()
            {
                Text = _.Name,
                Value = _.Id.ToString()
            }).ToList()??new List<SelectListItem>();
            permissions.Insert(0,new SelectListItem(){Value = "0",Text = "无"});
            ViewBag.ParentPermissions = permissions;
            return View(new Permission());
        }
        [HttpPost]
        public ActionResult AddPermission(Permission model)
        {
            if(ModelState.IsValid)
                _permissionManager.Add(model);
            return RedirectToAction("Index", "Permission");
        }
    }
}