using ResortManagement.Areas.Dashboard.Models;
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
        public ActionResult Rooms(int? pageNo,string CheckIn,string CheckOut,int? Adults, int? Children)
        {
            var d = DateTime.MinValue;
            var t = DateTime.MaxValue;
            var f = DateTime.Now;
            var g = f.AddDays(4);
            var j = f.Day;
            var i = f.Date;
            f.ToLongDateString();
            model.PageNo = pageNo ?? 1;
            model.PageSize = 4;
            bool CheckInisDate = DateTime.TryParse(CheckIn, out model.CheckIn);
            bool CheckOutisDate = DateTime.TryParse(CheckOut, out model.CheckOut);
            if (CheckInisDate & CheckOutisDate) { model.Duration = model.CheckOut.Day-model.CheckIn.Day; }
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
            model.accommodations = AccoommodationsService.Instance.GetAccommodationsByBooking(model.CheckIn, model.Duration,model.NoOfBeds, model.PageNo, model.PageSize);
            model.accommodations = AccoommodationsService.Instance.GetAllAccommodations(null, 0, model.PageSize, pageNo: model.PageNo);
            model.Accommopictures = PictureServices.Instance
                .GetPituresByPictureID(model.accommodations.
                Select(acc=>acc.accommodationPictures.Select(accp=>accp.pictureID).ToList()).SelectMany(x=>x).Distinct().ToList());
            var TotalAccommodations = AccoommodationsService.Instance.GetAllAccommodationsCount(null, 0);

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
        public ActionResult booking()
        {
            return View();
        }
    }
}