﻿using ResortManagement.Areas.Dashboard.Models;
using ResortManagement.Entities;
using ResortManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResortManagement.Areas.Dashboard.Controllers
{
    public class AccommodationGadgetsController : Controller
    {
        AccommodationGadgetsEditViewModel Model=new AccommodationGadgetsEditViewModel();
        // GET: Dashboard/AccommodationGatgets
        public ActionResult Index()
        {
            AccommondationGadgetsViewModel model = new AccommondationGadgetsViewModel();
            model.accommodationGadgetsEditViewModel.accommodationTypes = AccommodationTypeServices.Instance.GetAllAccommondationTypes();

            return View(model);
        }

        public ActionResult Listing(string searchTerm,int? accomoodationType,int? pagSize,int? pagNo)
        {
            AccommodationGadgetsListingViewModel model = new AccommodationGadgetsListingViewModel();
            model.SearchTerm = searchTerm;
            model.AccomoodationType = accomoodationType.HasValue?accomoodationType.Value>0?accomoodationType.Value:0:0;
            model.PageSize = pagSize.HasValue ? pagSize.Value > 5 ? pagSize.Value : 5 : 5;
            model.PageNo = pagNo.HasValue ? pagNo.Value > 0 ? pagNo.Value : 1 : 1;

            model.accommodationGadgets = AccommodationGadgetsServices.Instance.GetAllAccommodationGadgets(model.SearchTerm, model.AccomoodationType, model.PageSize, model.PageNo);
            int TotalGadgets = AccommodationGadgetsServices.Instance.GetAccommodationGadgetsCout(model.SearchTerm,model.AccomoodationType);
            model.pager = new Pager(TotalGadgets, model.PageNo, model.PageSize);

            model.accommodationTypes = AccommodationTypeServices.Instance.GetAllAccommondationTypes();
            return PartialView("_Listing", model);
        }

        public ActionResult Action(int? ID)
        {
            
            if (ID.HasValue)
            {
                Model.accommodationGadgets = AccommodationGadgetsServices.Instance.GetAccommodationGadgetsByID(ID.Value);
            }
            Model.accommodationTypes = AccommodationTypeServices.Instance.GetAllAccommondationTypes();

            return PartialView("_Action", Model);
        }
        [HttpPost]
        public JsonResult Action(AccommodationGatgets model)
        {
            JsonResult jsonResult = new JsonResult();

            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            bool Result = false;
           

                if (model.ID > 0)
                {
                    Model.accommodationGadgets = AccommodationGadgetsServices.Instance.GetAccommodationGadgetsByID(model.ID);
                    Model.accommodationGadgets.Name = model.Name;
                    Model.accommodationGadgets.NOfRoom = model.NOfRoom;
                    Model.accommodationGadgets.AccommodationTypeID = model.AccommodationTypeID;
                    Model.accommodationGadgets.accommodationType = AccommodationTypeServices.Instance.GetAccommondationTypeByID(model.AccommodationTypeID);
                    Result = AccommodationGadgetsServices.Instance.EditAccommondationGadget(model);
                    jsonResult.Data = new {Edited=Result, Success = Result, Message = Result ? "Accommodation Gadget updated successfully" : "update Accommodation Gadget Fail! Sorry.", Class = Result ? "alert-success" : "alert-danger" };
                }
                else
                {
                    Result = AccommodationGadgetsServices.Instance.CreateAccommondationGadget(model);
                    jsonResult.Data = new { Edited=false, Success = Result, Message = Result ? "Accommodation Gadget added successfully" : "add Accommodation Type Gadget! Sorry.", Class = Result ? "alert-success" : "alert-danger" };
                }

            
            return jsonResult;
        }
        [HttpPost]
        public JsonResult Delete(int? ID)
        {
            if(ID.HasValue)
            {
                return Json(new { Success = AccommodationGadgetsServices.Instance.DeleteAccommondationGadget(ID.Value) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound,"Resultat not Found!") }); 
            }
            
        }

    }
}