﻿using ResortManagement.Areas.Dashboard.Models;
using ResortManagement.Models;
using ResortManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResortManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ResortManagement.Models.AccommodationsViewModel model = new ResortManagement.Models.AccommodationsViewModel();
            model.PageNo = 1;
            model.PageSize = 4;
            model.accommodations = AccoommodationsService.Instance.GetAllAccommodations(string.Empty,0,model.PageSize, model.PageNo);
            model.Accommopictures = PictureServices.Instance.GetPituresByPictureID(model.accommodations.Select(acc=>acc.accommodationPictures.Select(accP=>accP.pictureID).ToList()).SelectMany(x=>x).Distinct().ToList());
            model.accommodationTypes = AccommodationTypeServices.Instance.GetAllAccommondationTypes();
            return View(model);
        }
        ResortManagement.Models.AccommodationsViewModel model = new ResortManagement.Models.AccommodationsViewModel();
        public ActionResult Rooms(string search,int? pageNo,string CheckIn,string CheckOut,int? Adults, int? Children)
        {
            model.PageNo = pageNo ?? 1;
            model.PageSize = 4;
            model.Adults = Adults.HasValue ? Adults.Value : 0;
            model.Children = Children.HasValue ? Children.Value : 0;
            bool CheckInisDate = DateTime.TryParse(CheckIn, out model.CheckIn);
            bool CheckOutisDate = DateTime.TryParse(CheckOut, out model.CheckOut);
            if (CheckInisDate && CheckOutisDate) { model.Duration =int.Parse(model.CheckOut.Subtract(model.CheckIn).Days.ToString()); }
            if (Adults.HasValue)
            {
                if (Children.HasValue)
                {
                    model.NoOfBeds = Adults + Children;
                }
                else
                {
                    model.NoOfBeds = Adults.Value;
                }
            }
            model.accommodations = AccoommodationsService.Instance.GetAccommodationsByBooking(search, model.CheckIn, model.Duration,model.NoOfBeds, model.PageNo, model.PageSize);
            model.Accommopictures = PictureServices.Instance
                .GetPituresByPictureID(model.accommodations.
                Select(acc=>acc.accommodationPictures.Select(accp=>accp.pictureID).ToList()).SelectMany(x=>x).Distinct().ToList());
            var TotalAccommodations = AccoommodationsService.Instance.GetAccommodationsByBookingCount(search, model.CheckIn, model.Duration, model.NoOfBeds);
            model.pager = new Pager(TotalAccommodations, model.PageNo, model.PageSize);
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult booking()
        {
         return View();
        }
        [HttpPost]
        public JsonResult booking(int? Id)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (Id.HasValue)
            {
                if (User.Identity.IsAuthenticated)
                {
                    //BookingsServices.Instance.CreateBookings();
                    jsonResult.Data = new { };
                }
                else
                {
                    jsonResult.Data = new {RedirectUrl = Url.Action("Login","Account") };
                }
            }
            else
            {
                jsonResult.Data = new {RedirectUrl = Url.Action("Rooms","Home") };
            }
            return jsonResult;
        }
    }
}