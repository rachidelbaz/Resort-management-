using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using ResortManagement.Areas.Dashboard.Models;
using ResortManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ResortManagement.Areas.Dashboard.Controllers
{
    public class RolesController : Controller
    {


        private ResortManagemenetSignInManager _signInManager;
        private ResortManagementUserManager _userManager;
        private ResortManagementRoleManager _roleManager;

        public RolesController()
        {
        }

        public RolesController(ResortManagementUserManager userManager, ResortManagemenetSignInManager signInManager ,ResortManagementRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public ResortManagemenetSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ResortManagemenetSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ResortManagementUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ResortManagementUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ResortManagementRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ResortManagementRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: Dashboard/Roles
        EditRolesViewModel Model = new EditRolesViewModel();
        public ActionResult Index()
        {
            RolesViewModel model = new RolesViewModel();
            return View(model);
        }
        public ActionResult Listing(string SearchTerm, int? pageSize, int? pageNo)
        {
            ListingRolesViewModel model = new ListingRolesViewModel();
            model.SearchTerm = !string.IsNullOrEmpty(SearchTerm)?SearchTerm.Trim():string.Empty;
            model.PageSize = pageSize.HasValue ? pageSize.Value > 5 ? pageSize.Value : 5 : 5;
            model.PageNo = pageNo.HasValue ? pageNo.Value > 1 ? pageNo.Value : 1 : 1;
            model.Roles = RoleManager.SearchRoles(model.SearchTerm,model.PageSize, model.PageNo);
            int Totalroles = RoleManager.RolesCount(model.SearchTerm);
            model.pager = new Pager(Totalroles, model.PageNo, model.PageSize);
            return PartialView("_Listing", model);
        }


        public async Task<ActionResult> Action(string ID)
        {
            if (!string.IsNullOrEmpty(ID))
            {
                   Model.role =await RoleManager.FindByIdAsync(ID);
            }

            //Model.uersList = AccoommodationsService.Instance.GetAllAccommodations();
            return PartialView("_Action", Model);
        }
        [HttpPost]
        public async Task<JsonResult> Action(IdentityRole model)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var msg = "<ul>";
            var _class = string.Empty;
            bool edit = false;
            if (ModelState.IsValid)
            {
                var IdResult = new IdentityResult();
                var Role = new IdentityRole();
                if (!string.IsNullOrEmpty(model.Id))
                {
                    Role = await RoleManager.FindByIdAsync(model.Id);
                    Role.Name = model.Name;
                    IdResult = await RoleManager.UpdateAsync(Role);
                    if (IdResult.Succeeded)
                    {
                        edit = true;
                        _class = "alert-success text-white";
                        msg = "Role updated successfully";
                    }
                    else
                    {
                        _class = "alert-danger text-white";

                        foreach (var error in IdResult.Errors)
                        {
                            msg += "<li>" + error + "</li>";
                        }
                        msg += "</ul>";
                    }
                }
                else
                {
                    Role.Name = model.Name;
                    IdResult = await RoleManager.CreateAsync(Role);
                    if (IdResult.Succeeded)
                    {
                        _class = "alert-success text-white";
                        msg = "Role added successfully";
                    }
                    else
                    {
                        _class = "alert-danger text-white";
                        foreach (var error in IdResult.Errors)
                        {
                            msg += "<li>" + error + "</li>";
                        }
                        msg += "</ul>";

                    }

                }
            }
            jsonResult.Data = new { Edited = edit, Message = msg, Class = _class };
            return jsonResult;

        }
        [HttpPost]
        public async Task<JsonResult> Delete(string ID)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(ID))
            {
                var Role =await RoleManager.FindByIdAsync(ID);
                var result = await RoleManager.DeleteAsync(Role);
                jsonResult.Data = new { Success = result.Succeeded };
            }
            return jsonResult;
        }
    }
}