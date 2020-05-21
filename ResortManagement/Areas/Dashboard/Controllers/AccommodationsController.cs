using ResortManagement.Areas.Dashboard.Models;
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
            return View(model);
        }
        public ActionResult Listing(string SearchTerm,string AccomadationGadgets,int? pageSize,int? pageNo)
        {
            AccommodationsListingViewModel model = new AccommodationsListingViewModel();
            model.SearchTerm = SearchTerm;
            model.AccommodationGatgets = AccomadationGadgets;
            model.PageSize = pageSize.HasValue?pageSize.Value>5?pageSize.Value:5:5;
            model.PageNo = pageNo.HasValue ? pageNo.Value > 1 ? pageNo.Value : 1 : 1;
            model.accommodations = AccoommodationsService.Instance.GetAllAccommodations(model.SearchTerm, model.AccommodationGatgets, model.PageSize, model.PageNo);
            int TotalAccomodations = AccoommodationsService.Instance.GetAllAccommodationsCount(model.SearchTerm, model.AccommodationGatgets);
            model.pager = new Pager(TotalAccomodations,model.PageNo,model.PageSize);
            return PartialView("_Listing",model);
        }
        public ActionResult Action(int? ID)
        { 
            if(ID.HasValue)
            {
                if (ID.Value>0)
                {
                    Model.accommodations = AccoommodationsService.Instance.GetAccommodationsByID(ID.Value);
                }
            }
            

            return PartialView("_Action",Model);
        }
    }
}