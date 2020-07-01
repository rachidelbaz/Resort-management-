using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ResortManagement.Areas.Dashboard.Models;
using ResortManagement.Entities;
using ResortManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResortManagement.Areas.Dashboard.Controllers
{
    public class BookingsController : Controller
    {
            BookingsEditViewModel Model = new BookingsEditViewModel();
        // GET: Dashboard/AccommodationTypes
        public ActionResult Index()
         {
            BookingsViewModel model = new BookingsViewModel();
            return View(model);
        }
        public ActionResult Listing(string searchTerm,string Status, int? pagSize, int? pagNo)
        {
            BookingsListingViewModel model = new BookingsListingViewModel();
            model.PageSize= pagSize.HasValue ? pagSize.Value > 0 ? pagSize.Value : 5 : 5;
            model.SearchTerm = searchTerm;
            model.PageNo = pagNo.HasValue ? pagNo.Value > 0 ? pagNo.Value : 1 : 1;
            model.Status = Status;
            model.Bookings = BookingsServices.Instance.GetSearchBookings(model.SearchTerm, model.Status, model.PageNo, model.PageSize);
            int TotalItems = BookingsServices.Instance.GetSearchBookingsCount(model.SearchTerm, model.Status);

            model.pager = new Pager(TotalItems, model.PageNo, model.PageSize);

            return PartialView("_Listing", model);
        }
        [HttpPost]
        public JsonResult Action(Bookings model)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            bool Result;
            //if (ModelState.isValid)
            //{

            if (model.ID > 0)
            {
                var UserManager = Request.GetOwinContext().GetUserManager<ResortManagementUserManager>();
                var user = UserManager.GetUserByCIN(model.RMUserId);
                if (user!=null)
                {
                    model.RMUserId = user.Id;
                    Result = BookingsServices.Instance.EditBookings(model);
                    jsonResult.Data = new { Edited = Result, Success = Result, Message = Result ? "Booking updated successfully" : "update Booking Failed! Sorry.", Class = Result ? "alert-success" : "alert-danger" };
                }
                else
                {
                    jsonResult.Data = new { Edited = true, Success = false, Message = "update Booking Failed! Sorry! user not fond !", Class = "alert-danger" };
                }

            }
            else
            {
                
                if (!string.IsNullOrEmpty(model.RMUserId))
                {
                    var UserManager = Request.GetOwinContext().GetUserManager<ResortManagementUserManager>();
                     var user = UserManager.GetUserByCIN(model.RMUserId);
                    if(user!=null)
                    {
                        model.RMUserId = user.Id;
                        Result = BookingsServices.Instance.CreateBookings(model);
                        jsonResult.Data = new { Edited = false, Success = Result, Message = Result ? "Booking added successfully" : "add Booking Failed! Sorry.", Class = Result ? "alert-success" : "alert-danger" };
                    }
                    else
                    {
                        jsonResult.Data = new { Edited = false, Success = false, Message ="add Booking Failed! Sorry! user not fond !", Class ="alert-danger" };
                    }

                }
                
            }
            //}
            //else
            //{
            //    var e = ModelState.Select(Er=>Er.Value.Errors).Where(y=>y.Count()>0).ToList();
            //}

           
            return jsonResult;
        }
        ///// <summary>
        ///// Action get work for create and edit 
        ///// </summary>
        [HttpGet]
        public ActionResult Action(int? ID)
        {

            if (ID.HasValue)
            {
                Model.booking = BookingsServices.Instance.GetBookingsByID(ID.Value);
                var userManager = Request.GetOwinContext().GetUserManager<ResortManagementUserManager>();
                var user= userManager.GetUserByID(Model.booking.RMUserId);
                Model.booking.RMUserId =user.CIN;
            }
            Model.accommodationTypes = AccommodationTypeServices.Instance.GetAllAccommondationTypes().OrderBy(acc=>acc.Type).ToList();
            return PartialView("_Action", Model);

        }
        [HttpPost]
        public JsonResult Delete(int ID)
        {
            return Json(new { Success = BookingsServices.Instance.DeleteBookingsByID(ID) }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetGadgetByType(int?Id)
        {
            JsonResult jsonResut = new JsonResult();
            jsonResut.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (Id.HasValue)
            {
               // var Gadg = AccommodationGadgetsServices.Instance.GetGAdgetByAccTypeID(Id.Value);
                jsonResut.Data = new { Gadgets = AccommodationGadgetsServices.Instance.GetGAdgetByAccTypeID(Id.Value) };
            }

            return jsonResut;
        }
        public JsonResult GetAccByGadget(int?Id)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (Id.HasValue)
            {
                jsonResult.Data = new {Accs= AccoommodationsService.Instance.GetAccByGadgetId(Id.Value) };
            }
            return jsonResult;
        }

        public JsonResult CheckAccommoDate(int?Id)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (Id.HasValue)
            {
                jsonResult.Data =new {Date= BookingsServices.Instance.GetAccoAvailableDate(Id.Value) };
            }
            return jsonResult;
        }
    }
}