using ResortManagement.Areas.Dashboard.Models;
using ResortManagement.Entities;
using ResortManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

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
            var urls=new List<string>();
            var newPics = new List<Picture>();
            var newAccomodation = new Accommodations();
            newAccomodation.Name= model.Name;
            newAccomodation.Description=model.Description;
            newAccomodation.AccommodationGatgetID= model.AccommodationGatgetID;

            if (!string.IsNullOrEmpty(model.imgUrls))
            {
                foreach (var itemUrl in model.imgUrls.Split(','))
                {
                    urls.Add(itemUrl.Split('/').LastOrDefault());
                }
                foreach (var url in urls)
                {
                    var newPic = new Picture() { URL = url };
                    var IsAdded = PictureServices.Instance.CreatePicture(newPic);
                    if (IsAdded)
                    {
                        newPics.Add(newPic);
                    }

                }
            }
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            bool Result = false;
            if (model.ID > 0)
            {
                var OldAccommodationPictures = AccoommodationsService.Instance.GetAccommodationsByID(model.ID).accommodationPictures;
                if (OldAccommodationPictures != null && OldAccommodationPictures.Any())
                {
                    var pics = PictureServices.Instance.GetPituresByPictureID(OldAccommodationPictures.Select(old => old.pictureID).ToList());
                    bool isDeleted = PictureServices.Instance.DeletePics(OldAccommodationPictures.Select(acc => acc.pictureID).ToList());
                    foreach (var item in pics)
                    {    
                        var filePath = Server.MapPath(string.Concat("/content/images/WebPictures/", item.URL));
                        System.IO.File.Delete(filePath);
                    }
                }
                newAccomodation.ID = model.ID;
                newAccomodation.accommodationPictures = new List<AccommodationPicture>();
                newAccomodation.accommodationPictures.AddRange(newPics.Select(pic => new AccommodationPicture() { pictureID = pic.ID, AccommodationID = model.ID }));
      
                Result = AccoommodationsService.Instance.EditAccommodation(newAccomodation);
               jsonResult.Data = new { Edited = Result, Success = Result, Message = Result ? "Accommodation updated successfully" : "update Accommodation Fail! Sorry.", Class = Result ? "alert-success" : "alert-danger" };
            }
            else
            { 
                newAccomodation.accommodationPictures = new List<AccommodationPicture>();
                newAccomodation.accommodationPictures.AddRange(newPics.Select(pic=>new AccommodationPicture() { pictureID=pic.ID, AccommodationID=newAccomodation.ID}));
                Result = AccoommodationsService.Instance.CreatAccommodation(newAccomodation);
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