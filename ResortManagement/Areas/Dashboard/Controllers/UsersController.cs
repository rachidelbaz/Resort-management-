using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using ResortManagement.Areas.Dashboard.Models;
using ResortManagement.DataBase;
using ResortManagement.Entities;
using ResortManagement.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ResortManagement.Areas.Dashboard.Controllers
{
    public class UsersController : Controller
    {
        private ResortManagemenetSignInManager _signInManager;
        private ResortManagementUserManager _userManager;
        private ResortManagementRoleManager _roleManager;
        public UsersController()
        {
        }
        public UsersController(ResortManagementUserManager userManager, ResortManagemenetSignInManager signInManager, ResortManagementRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
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

        UserEditViewmodel Model = new UserEditViewmodel();

        // GET: Dashboard/Users
        public ActionResult Index()
        {
            UsersViewmodel model = new UsersViewmodel();
            model.Roles = UserManager.GetAllRoles();
            return View(model);
        }
        public ActionResult Listing(string SearchTerm, string RoleID, int? pageSize, int? pageNo)
        {
            UsersListingViewmodel model = new UsersListingViewmodel();
            model.SearchTerm = SearchTerm;
            model.RoleID = !string.IsNullOrEmpty(RoleID) ? RoleID : string.Empty;
            model.PageSize = pageSize.HasValue ? pageSize.Value > 5 ? pageSize.Value : 5 : 5;
            model.PageNo = pageNo.HasValue ? pageNo.Value > 1 ? pageNo.Value : 1 : 1;
            var rMUsersWithR = new List<RMUser>();
            model.RMUsers= UserManager.GetUsers(model.SearchTerm, model.RoleID, model.PageSize, model.PageNo);
            rMUsersWithR.AddRange(model.RMUsers.Where(u=>u.Roles.Any()));
            rMUsersWithR.AddRange(model.RMUsers.Where(u =>!u.Roles.Any()));
            model.RMUsers = rMUsersWithR;
            int TotalUsers = UserManager.GetUsersCount(model.SearchTerm, model.RoleID);
            model.pager = new Pager(TotalUsers, model.PageNo, model.PageSize);
            model.AllRoles = UserManager.GetAllRoles();
            return PartialView("_Listing", model);
        }

        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {

            if (!string.IsNullOrEmpty(ID))
            {
                Model.RMUser = await UserManager.FindByIdAsync(ID);

                if (Model.RMUser != null)
                {
                    Model.RMUser.UserRoles = await UserManager.GetRolesAsync(Model.RMUser.Id);
                }
            }

            Model.Roles = UserManager.GetAllRoles();
            return PartialView("_Action", Model);
        }
        [HttpPost]
        public async Task<JsonResult> Action(RMUser model)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var msg = "<ul>";
            var _class = string.Empty;
            bool edit = false;
            //if (ModelState.IsValid)
            //{
            IdentityResult IdResult, resultRole;
            var user = new RMUser();

            if (!string.IsNullOrEmpty(model.Id))
            {
                user = await UserManager.FindByIdAsync(model.Id);

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.City = model.City;
               
                user.DateOfBirth = model.DateOfBirth;
                user.Address = model.Address;
                if (model.UserRoles!=null)
                {
                    foreach (var UroleName in model.UserRoles)
                    {
                        if (!(await UserManager.IsInRoleAsync(user.Id, UroleName)))
                        {
                            resultRole = await UserManager.AddToRoleAsync(model.Id, UroleName);
                        }
                    }
                    foreach (var Ur in user.Roles)
                    {
                        var r = await RoleManager.FindByIdAsync(Ur.RoleId);
                        if (!model.UserRoles.Contains(r.Name))
                        {
                            resultRole = await UserManager.RemoveFromRoleAsync(model.Id, r.Name);
                        }

                    }
                }


                IdResult = await UserManager.UpdateAsync(user);

                if (IdResult.Succeeded)
                {
                    edit = true;
                    _class = "alert-success text-white";
                    msg = "User updated successfully";
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
                user.CIN = model.CIN;
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.City = model.City;
                user.DateOfBirth = model.DateOfBirth;
                user.Address = model.Address;

                IdResult = await UserManager.CreateAsync(user);

                if (IdResult.Succeeded)
                {
                    foreach (var UroleName in model.UserRoles)
                    {
                        resultRole = await UserManager.AddToRoleAsync(user.Id, UroleName);
                    }
                    _class = "alert-success text-white";
                    msg = "User added successfully";
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
                var user = UserManager.FindById(ID);
                var result = await UserManager.DeleteAsync(user);
                jsonResult.Data = new { Success = result.Succeeded };
                //jsonResult.Data=new { Success =await UserManager.DeleteUserByID(ID) };
            }


            return jsonResult;
        }


    }
}








