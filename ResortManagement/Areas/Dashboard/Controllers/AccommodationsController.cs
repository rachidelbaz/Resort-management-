﻿using ResortManagement.Areas.Dashboard.Models;
using ResortManagement.Entities;
using ResortManagement.Services;
using System.Collections.Generic;
using System.Linq;
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
                    Model.Pictures= PictureServices.Instance.GetPituresByPictureID(Model.accommodation.accommodationPictures.Select(acc=>acc.pictureID).ToList());
                }
            }

            Model.accommodationsList = AccoommodationsService.Instance.GetAllAccommodations();
            Model.accommodationGatgets = AccommodationGadgetsServices.Instance.GetAllAccommodationGadgets();
            return PartialView("_Action", Model);
        }
        [HttpPost]
        public JsonResult Action(AccommodationActionModel model)
        {
            var newPics = new List<Picture>();
            var newAccomodation = new Accommodations();
            newAccomodation.ID = model.ID;
            newAccomodation.Name= model.Name;
            newAccomodation.Description=model.Description;
            newAccomodation.AccommodationGatgetID= model.AccommodationGatgetID;

            if (!string.IsNullOrEmpty(model.imgUrls))
            {
                newPics.AddRange(PictureServices.Instance.converterToPictures(model.imgUrls));
            }

            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            bool Result = false;
            if (model.ID > 0)
            {

                Model.accommodation= AccoommodationsService.Instance.GetAccommodationsByID(model.ID);
                if (Model.accommodation.accommodationPictures != null && Model.accommodation.accommodationPictures.Any())
                {
                    bool isDeleted = PictureServices.Instance.DeletePics(Model.accommodation.accommodationPictures.Select(acc => acc.pictureID).ToList());     
                }
                if (newPics.Any())
                {
                    newAccomodation.accommodationPictures = new List<AccommodationPicture>();
                    newAccomodation.accommodationPictures.AddRange(newPics.Select(pic => new AccommodationPicture() { pictureID = pic.ID, AccommodationID = model.ID }));
                }
                
                Result = AccoommodationsService.Instance.EditAccommodation(newAccomodation, model.Name);
               jsonResult.Data = new { Edited = Result, Success = Result, Message = Result ? "Accommodation updated successfully" : "update Accommodation Failed! Sorry.", Class = Result ? "alert-success" : "alert-danger" };
            }
            else
            { 
                newAccomodation.accommodationPictures = new List<AccommodationPicture>();
                newAccomodation.accommodationPictures.AddRange(newPics.Select(pic=>new AccommodationPicture() { pictureID=pic.ID, AccommodationID=newAccomodation.ID}));
                Result = AccoommodationsService.Instance.CreatAccommodation(newAccomodation);
                jsonResult.Data = new { Edited = false, Success = Result, Message = Result ? "Accommodation Added successfully" : "Add Accommodation Failed! Sorry.", Class = Result ? "alert-success" : "alert-danger" };
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