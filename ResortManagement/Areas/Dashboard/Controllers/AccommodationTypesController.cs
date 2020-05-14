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
    public class AccommodationTypesController : Controller
    {
        // GET: Dashboard/AccommodationTypes
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Listing(string searchTerm, int? pagNo)
        {
            int pageSize = 5;
            AccommondationTypesViewModel model = new AccommondationTypesViewModel();
            model.SearchTerm = searchTerm;
            model.PageNo = pagNo.HasValue ? pagNo.Value > 0 ? pagNo.Value : 1 : 1;

            model.accommodationTypes = AccommodationTypeServices.Instance.GetSearchAccommondationTypes(searchTerm, model.PageNo);
            int TotalItems = AccommodationTypeServices.Instance.GetSearchAccommondationTypesCount(searchTerm);

            model.pager = new Pager(TotalItems, model.PageNo, pageSize);

            return PartialView("_Listing", model);
        }
        [HttpPost]
        public JsonResult Action(AccommodationTypes model)
        {
            JsonResult jsonResult = new JsonResult();

            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (ModelState.IsValid)
            {
                var Result = AccommodationTypeServices.Instance.CreateAccommondationType(model);

                jsonResult.Data = new { Success = Result, Message = Result ? "Accommodation Type added successfully" : "add Accommodation Type Fail! Sorry.", Class = Result?"alert-success" : "alert-danger" };
                
            }
            return jsonResult;
        }
        [HttpGet]
        public ActionResult Action()
        {
            return PartialView("_Action");
        }



    }
}