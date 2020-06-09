using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResortManagement.Areas.Dashboard.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard/Dashboard
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UpLoadPictures()
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var Files = Request.Files;
            var URLpictures = new List<string>();
            try
            {
                for (int i = 0; i < Files.Count; i++)
                {
               
                    var picture = Files[i];
                    var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Content/images/WebPictures"), fileName);
                    URLpictures.Add(string.Concat("/Content/images/WebPictures/", fileName));
                    picture.SaveAs(filePath);
                    
                }
                jsonResult.Data = new { success = true, ImgURL = URLpictures };

            }
            catch (Exception x)
            {
                jsonResult.Data = new { success = false, message = x.Message };
            }
          

            return jsonResult;
        }
    }
}