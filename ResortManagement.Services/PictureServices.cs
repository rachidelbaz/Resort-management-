using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ResortManagement.DataBase;
using ResortManagement.Entities;

namespace ResortManagement.Services
{
    public class PictureServices
    {
        public static PictureServices Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PictureServices();
                }
                return instance;
            }
        }
        private static PictureServices instance { get; set; }

        public bool CreatePicture(Picture newPic)
        {
            using (var context = new ResortManagementDbContext())
            {
                context.picture.Add(newPic);
                return context.SaveChanges() > 0;
            }
        }

        public List<Picture> GetPituresByPictureID(List<int> picIDs)
        {
            using (var context = new ResortManagementDbContext())
            {
                return context.picture.Where(p => picIDs.Contains(p.ID)).ToList();
            }
        }

        public bool DeletePics(List<int> list)
        {
            using (var context = new ResortManagementDbContext())
            {
                foreach (var item in GetPituresByPictureID(list))
                {
                    try { var filePath = HttpContext.Current.Server.MapPath(string.Concat("/content/images/WebPictures/", item.URL));
                        System.IO.File.Delete(filePath);} catch {}  
                    context.picture.Attach(item);
                    context.picture.Remove(item);
                }
                return context.SaveChanges() > 0;
            }
        }
        public List<Picture> converterToPictures(string urls)
        {
            var pictures=new List<Picture>(); 
            var imgUrls = new List<string>();

            foreach (var itemUrl in urls.Split(','))
            {
              imgUrls.Add(itemUrl.Split('/').LastOrDefault());
            }
            foreach (var url in imgUrls)
            {
                var newPic = new Picture() { URL = url };
                var IsAdded = CreatePicture(newPic);
                if (IsAdded)
                {
                    pictures.Add(newPic);
                }

            }
            return pictures;

        }
    }
}
