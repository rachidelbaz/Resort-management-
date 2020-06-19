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
            List<bool> results = new List<bool>();
            try
            {
                for (int i = 0; i < Files.Count; i++)
                {
                    if (Files[i].ContentLength/1024>30 && Files[i].ContentLength/1024<400)
                    {
                        var picture = Files[i];
                        var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);
                        var filePath = Path.Combine(Server.MapPath("~/Content/images/WebPictures"), fileName);
                        URLpictures.Add(string.Concat("/Content/images/WebPictures/", fileName));
                        picture.SaveAs(filePath);
                        results.Add(true);
                    }
                    else
                    {
                        results.Add(false);
                    }
                       
                }
                if (!results.Contains(true))
                {
                    jsonResult.Data = new { success = false, message = "Non valide One or more image, minimum size 30 Kb & maximum size 400 Kb",Class= "alert-danger" };
                }
                else
                {
                    jsonResult.Data = new { success = true, ImgURL= URLpictures };

                }
            }
            catch (Exception x)
            {
                jsonResult.Data = new { success = false, message = x.Message };
            }
          

            return jsonResult;
        }

        [HttpPost]
        public JsonResult DeletePic(string imgName)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(imgName))
            {
                try
                {
                    var file = Server.MapPath(imgName);
                    System.IO.File.Delete(file);
                    jsonResult.Data = new { seccuss=true };
                }
                catch {
                    jsonResult.Data = new { seccuss = false };
                }
            }
            return jsonResult;
        }
    }
}