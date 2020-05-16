using ResortManagement.Areas.Dashboard.Models;
using ResortManagement.Entities;
using ResortManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ResortManagement.Areas.Dashboard.Controllers
{
    public class AccommodationTypesController : Controller
    {
        AccommondationTypeEditViewModel Model = new AccommondationTypeEditViewModel();
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

            bool Result = false;
            if (ModelState.IsValid)
            {
               
                if (model.ID>0)
                {
                    Model.accommodationType = AccommodationTypeServices.Instance.GetAccommondationTypeByID(model.ID);
                    Model.accommodationType.Type = model.Type;
                    Model.accommodationType.Description = model.Description;
                    Result = AccommodationTypeServices.Instance.EditAccommondationType(model);
                    jsonResult.Data = new { Success = Result, Message = Result ? "Accommodation Type updated successfully" : "update Accommodation Type Fail! Sorry.", Class = Result ? "alert-success" : "alert-danger" };
                }
                else
                {
                    Result = AccommodationTypeServices.Instance.CreateAccommondationType(model);
                    jsonResult.Data = new { Success = Result, Message = Result ? "Accommodation Type added successfully" : "add Accommodation Type Fail! Sorry.", Class = Result ? "alert-success" : "alert-danger" };
                }
   
            }
            return jsonResult;
        }
        /// <summary>
        /// Action get work for create and edit 
        /// </summary>
        [HttpGet]
        public ActionResult Action(int? ID)
        {

            if (ID.HasValue)
            {
                JsonResult result = new JsonResult();
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

                Model.accommodationType = AccommodationTypeServices.Instance.GetAccommondationTypeByID(ID.Value);
                result.Data = new { Success = Model.accommodationType!=null,
                                    ID = Model.accommodationType.ID, 
                                    Type= Model.accommodationType.Type,
                                    Description= Model.accommodationType.Description };
                return result;
            }

            return PartialView("_Action");

        }

    }
}