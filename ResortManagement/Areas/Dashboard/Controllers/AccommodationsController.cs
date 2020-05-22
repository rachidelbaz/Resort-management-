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
    public class AccommodationsController : Controller
    {
        AccommodationsEditViewModel Model = new AccommodationsEditViewModel();
        // GET: Dashboard/Accommodations
        
        public ActionResult Index()
        {
            AccommodationsViewModel model = new AccommodationsViewModel();
            model.accommodationGatgets=AccommodationGadgetsServices.Instance.GetAllAccommodationGadgets();
            return View(model);
        }
        public ActionResult Listing(string SearchTerm, int? AccomadationGadgetID, int? pageSize, int? pageNo)
        {
            AccommodationsListingViewModel model = new AccommodationsListingViewModel();
            model.SearchTerm = SearchTerm;
            model.AccommodationGatgetID = AccomadationGadgetID.HasValue ? AccomadationGadgetID.Value > 0 ? AccomadationGadgetID.Value : 0 : 0;
            model.PageSize = pageSize.HasValue ? pageSize.Value > 5 ? pageSize.Value : 5 : 5;
            model.PageNo = pageNo.HasValue ? pageNo.Value > 1 ? pageNo.Value : 1 : 1;
            model.accommodations = AccoommodationsService.Instance.GetAllAccommodations(model.SearchTerm, model.AccommodationGatgetID, model.PageSize, model.PageNo);
            int TotalAccomodations = AccoommodationsService.Instance.GetAllAccommodationsCount(model.SearchTerm, model.AccommodationGatgetID);
            model.pager = new Pager(TotalAccomodations, model.PageNo, model.PageSize);
            return PartialView("_Listing", model);
        }
        public ActionResult Action(int? ID)
        {
            if (ID.HasValue)
            {
                if (ID.Value > 0)
                {
                    Model.accommodation = AccoommodationsService.Instance.GetAccommodationsByID(ID.Value);
                }
            }

            Model.accommodationsList = AccoommodationsService.Instance.GetAllAccommodations();
            Model.accommodationGatgets = AccommodationGadgetsServices.Instance.GetAllAccommodationGadgets();
            return PartialView("_Action", Model);
        }
        [HttpPost]
        public JsonResult Action(Accommodations model)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            bool Result = false;
            if (model.ID > 0)
            {
                Result = AccoommodationsService.Instance.EditAccommodation(model);
                jsonResult.Data = new { Edited = Result, Success = Result, Message = Result ? "Accommodation updated successfully" : "update Accommodation Fail! Sorry.", Class = Result ? "alert-success" : "alert-danger" };

            }
            else
            {
               
                Result = AccoommodationsService.Instance.CreatAccommodation(model);
            jsonResult.Data = new { Edited = false, Success = Result, Message = Result ? "Accommodation updated successfully" : "update Accommodation Fail! Sorry.", Class = Result ? "alert-success" : "alert-danger" };

            }

            return jsonResult;

        }
        [HttpPost]
        public JsonResult Delete(int? ID)
        {

            if (ID.HasValue)
            {
                return Json(new { Succes = AccoommodationsService.Instance.DeleteAccommodationByID(ID.Value),JsonRequestBehavior.AllowGet });
            }


            return Json(new {Succes=AccoommodationsService.Instance.DeleteAccommodationByID(ID.Value) });
        }
    }
}