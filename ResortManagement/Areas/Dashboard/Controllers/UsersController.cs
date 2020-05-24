using Microsoft.AspNet.Identity.Owin;
using ResortManagement.Areas.Dashboard.Models;
using ResortManagement.DataBase;
using ResortManagement.Entities;
using ResortManagement.Services;
using System;
using System.Collections.Generic;
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
        public UsersController()
        {
        }
        public UsersController(ResortManagementUserManager userManager, ResortManagemenetSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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
     

        UserEditViewmodel Model = new UserEditViewmodel();
       
        // GET: Dashboard/Users
        public ActionResult Index()
        {
            UsersViewmodel model = new UsersViewmodel();
            model.Roles = UserManager.GetAllRoles();
            return View(model);
        }
        public ActionResult Listing(string SearchTerm, int? RoleID, int? pageSize, int? pageNo)
        {
            UsersListingViewmodel model = new UsersListingViewmodel();
            model.SearchTerm = SearchTerm;
            model.RoleID = RoleID.HasValue ? RoleID.Value > 0 ? RoleID.Value : 0 : 0;
            model.PageSize = pageSize.HasValue ? pageSize.Value > 5 ? pageSize.Value : 5 : 5;
            model.PageNo = pageNo.HasValue ? pageNo.Value > 1 ? pageNo.Value : 1 : 1;
            model.RMUsers = UserManager.GetUsers(model.SearchTerm, model.RoleID,model.PageSize,model.PageNo);         
            int TotalUsers = UserManager.GetUsersCount(model.SearchTerm, model.RoleID);
            model.pager = new Pager(TotalUsers, model.PageNo, model.PageSize);
            return PartialView("_Listing", model);
        }

       
        public async Task<ActionResult> Action(string ID)
        {
           
                if (!string.IsNullOrEmpty(ID))
                {
                    Model.RMUser = UserManager.GetUserByID(ID);
                    Model.IRoles =await UserManager.GetRolesAsync(ID);
                   // Model.RMUser = UserManager.FindByIdAsync(ID.Value);
                }
            

             Model.Roles= UserManager.GetAllRoles();
            return PartialView("_Action", Model);
        }
        [HttpPost]
        public async Task<JsonResult> Action(RMUser model)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            bool Result = false;
            if (!string.IsNullOrEmpty(model.Id))
            {
                Result = await UserManager.EditUser(model); //UserManager.CreateAsync(model);
                jsonResult.Data = new { Edited = Result, Success = Result, Message = Result ? "User updated successfully" : "update User Fail! Sorry.", Class = Result ? "alert-success" : "alert-danger" };

            }
            else
            {

                Result =await UserManager.CreateUser(model);
                jsonResult.Data = new { Edited =false , Success = Result, Message = Result ? "User was added successfully" : "add User Fail! Sorry.", Class = Result ? "alert-success" : "alert-danger" };

            }

            return jsonResult;

        }
        [HttpPost]
        public JsonResult Delete(string ID)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(ID))
            {
                return Json(new { Succes = UserManager.DeleteUserByID(ID), jsonResult});
            }


            return jsonResult;
        }
    }
}