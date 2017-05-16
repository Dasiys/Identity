using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Infrastructure.Manager;
using Microsoft.AspNet.Identity.Owin;
using System.ComponentModel.DataAnnotations;
using Domain.Model;
using System.Threading.Tasks;
using Domain.Create;
using Domin.Create;
using Infrastructure.Database;

namespace Identity.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleAdminController : Controller
    {
        private readonly PermissionManager _permissionManager=new PermissionManager();
        // GET: RoleAdmin
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(new AppRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            AppRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToActionPermanent("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }

            }
            else
            {
                return View("Error", new string[] { "Role Not Found" });
            }
        }

        public async Task<ActionResult> Edit(string id)
        {
            AppRole role = await RoleManager.FindByIdAsync(id);
            string[] memberIds = role.Users.Select(m => m.UserId).ToArray();
            IEnumerable<AppUser> members = UserManager.Users.Where(x => memberIds.Any(y => y == x.Id));
            IEnumerable<AppUser> nonMembers = UserManager.Users.Except(members);
            return View(new RoleEditModel
            {
                Members = members,
                NonMembers = nonMembers,
                Role = role,
                Permission = _permissionManager.GetEditPermissions(role.Permissions?.ToList() )
            });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach(string userId in model.IdsToAdd??new string[] { })
                {
                    result = await UserManager.AddToRolesAsync(userId, model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                foreach(string userId in model.IdsToDelete??new string[] { })
                {
                    result = await UserManager.RemoveFromRolesAsync(userId, model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                var role = await RoleManager.FindByNameAsync(model.RoleName);
                RoleManager.AddRolePermission(role,model.PermissionIds,HttpContext.GetOwinContext().Get<AppIdentityDbContext>());
                RoleManager.Update(role);
                return RedirectToAction("Index");
            }
            return View("Errors", new string[] { "Role Not Found" });
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach(string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }


    }
}