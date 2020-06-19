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
            AccommondationTypesViewModel model = new AccommondationTypesViewModel();
            return View(model);
        }
        public ActionResult Listing(string searchTerm,int? pagSize, int? pagNo)
        {
            AccommondationListingTypesViewModel model = new AccommondationListingTypesViewModel();
            model.PageSize= pagSize.HasValue ? pagSize.Value > 0 ? pagSize.Value : 5 : 5;
            model.SearchTerm = searchTerm;
            model.PageNo = pagNo.HasValue ? pagNo.Value > 0 ? pagNo.Value : 1 : 1;

            model.accommodationTypes = AccommodationTypeServices.Instance.GetSearchAccommondationTypes(searchTerm, model.PageNo, model.PageSize);
            int TotalItems = AccommodationTypeServices.Instance.GetSearchAccommondationTypesCount(searchTerm);

            model.pager = new Pager(TotalItems, model.PageNo, model.PageSize);

            return PartialView("_Listing", model);
        }
        [HttpPost]
        public JsonResult Action(AccommondationTypesActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var newAccommodationTypes = new AccommodationTypes();
            newAccommodationTypes.ID = model.ID;
            newAccommodationTypes.Type = model.Type;
            newAccommodationTypes.Description = model.Description;
            var Pictures = new List<Picture>();
            if (!string.IsNullOrEmpty(model.imgUrls))
            {
                Pictures.AddRange(PictureServices.Instance.converterToPictures(model.imgUrls));
            }

            bool Result = false;
           
               
                if (model.ID>0)
                {
                    Model.accommodationType = AccommodationTypeServices.Instance.GetAccommondationTypeByID(model.ID);
                    if (Model.accommodationType.AccommodationTypePictures.Any())
                    {
                     bool IsDeleted = PictureServices.Instance.DeletePics(Model.accommodationType.AccommodationTypePictures.Select(acc=>acc.pictureID).ToList());
                    }
                    newAccommodationTypes.AccommodationTypePictures = new List<AccommodationTypePicture>();
                    newAccommodationTypes.AccommodationTypePictures.AddRange(Pictures.Select(p => new AccommodationTypePicture() { pictureID = p.ID, AccommodationTypeId = newAccommodationTypes.ID }));
                    Result = AccommodationTypeServices.Instance.EditAccommondationType(newAccommodationTypes);
                    jsonResult.Data = new { Edited = Result, Success = Result, Message = Result ? "Accommodation Type updated successfully" : "update Accommodation Type Failed! Sorry.", Class = Result ? "alert-success" : "alert-danger" };
                }
                else
                {
                     newAccommodationTypes.AccommodationTypePictures = new List<AccommodationTypePicture>();
                     newAccommodationTypes.AccommodationTypePictures.AddRange(Pictures.Select(p=>new AccommodationTypePicture() { pictureID=p.ID, AccommodationTypeId = newAccommodationTypes.ID }));
                    Result = AccommodationTypeServices.Instance.CreateAccommondationType(newAccommodationTypes);
                     jsonResult.Data = new { Edited = false, Success = Result, Message = Result ? "Accommodation Type added successfully" : "add Accommodation Type Failed! Sorry.", Class = Result ? "alert-success" : "alert-danger" };
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
                Model.accommodationType = AccommodationTypeServices.Instance.GetAccommondationTypeByID(ID.Value);
                Model.pictures = PictureServices.Instance.GetPituresByPictureID(Model.accommodationType.AccommodationTypePictures.Select(acc=>acc.pictureID).ToList()); 
            }

            return PartialView("_Action", Model);

        }
        [HttpPost]
        public JsonResult Delete(int ID)
        {
            return Json(new { Success = AccommodationTypeServices.Instance.DeleteAccommondationTypeByID(ID)}, JsonRequestBehavior.AllowGet);
        }

    }
}