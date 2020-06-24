using ResortManagement.Areas.Dashboard.Models;
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

            model.Bookings = BookingsServices.Instance.GetSearchBookings(model.SearchTerm, model.PageNo, model.PageSize);
            int TotalItems = BookingsServices.Instance.GetSearchBookingsCount(model.SearchTerm);

            model.pager = new Pager(TotalItems, model.PageNo, model.PageSize);

            return PartialView("_Listing", model);
        }
        //[HttpPost]
        //public JsonResult Action(BookingsActionViewModel model)
        //{
        //    JsonResult jsonResult = new JsonResult();
        //    jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        //    var newAccommodationTypes = new AccommodationTypes();
        //    newAccommodationTypes.ID = model.ID;
        //    newAccommodationTypes.Type = model.Type;
        //    newAccommodationTypes.Description = model.Description;
        //    var Pictures = new List<Picture>();
        //    if (!string.IsNullOrEmpty(model.imgUrls))
        //    {
        //        Pictures.AddRange(PictureServices.Instance.converterToPictures(model.imgUrls));
        //    }

        //    bool Result = false;


        //        if (model.ID>0)
        //        {
        //            Model.accommodationType = BookingsServices.Instance.GetBookingsByID(model.ID);
        //            if (Model.accommodationType.AccommodationTypePictures.Any())
        //            {
        //             bool IsDeleted = PictureServices.Instance.DeletePics(Model.accommodationType.AccommodationTypePictures.Select(acc=>acc.pictureID).ToList());
        //            }
        //            newAccommodationTypes.AccommodationTypePictures = new List<AccommodationTypePicture>();
        //            newAccommodationTypes.AccommodationTypePictures.AddRange(Pictures.Select(p => new AccommodationTypePicture() { pictureID = p.ID, AccommodationTypeId = newAccommodationTypes.ID }));
        //            Result = BookingsServices.Instance.EditBookings(newAccommodationTypes);
        //            jsonResult.Data = new { Edited = Result, Success = Result, Message = Result ? "Accommodation Type updated successfully" : "update Accommodation Type Failed! Sorry.", Class = Result ? "alert-success" : "alert-danger" };
        //        }
        //        else
        //        {
        //             newAccommodationTypes.AccommodationTypePictures = new List<AccommodationTypePicture>();
        //             newAccommodationTypes.AccommodationTypePictures.AddRange(Pictures.Select(p=>new AccommodationTypePicture() { pictureID=p.ID, AccommodationTypeId = newAccommodationTypes.ID }));
        //            Result = AccommodationTypeServices.Instance.CreateBookings(newAccommodationTypes);
        //             jsonResult.Data = new { Edited = false, Success = Result, Message = Result ? "Accommodation Type added successfully" : "add Accommodation Type Failed! Sorry.", Class = Result ? "alert-success" : "alert-danger" };
        //        }


        //    return jsonResult;
        //}
        ///// <summary>
        ///// Action get work for create and edit 
        ///// </summary>
        [HttpGet]
        public ActionResult Action(int? ID)
        {

            if (ID.HasValue)
            {
                Model.booking = BookingsServices.Instance.GetBookingsByID(ID.Value);
            }
            Model.accommodationTypes = AccommodationTypeServices.Instance.GetAllAccommondationTypes().OrderBy(acc=>acc.Type).ToList();
            return PartialView("_Action", Model);

        }
        [HttpPost]
        public JsonResult Delete(int ID)
        {
            return Json(new { Success = AccommodationTypeServices.Instance.DeleteBookingsByID(ID) }, JsonRequestBehavior.AllowGet);
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
    }
}