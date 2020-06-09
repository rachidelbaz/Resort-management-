using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            using (var context= new ResortManagementDbContext())
            {
                context.picture.Add(newPic);
                return context.SaveChanges() > 0;
            }
        }

        public List<Picture> GetPituresByPictureID(List<int> picIDs)
        {
            using (var context= new ResortManagementDbContext())
            {
                return context.picture.Where(p => picIDs.Contains(p.ID)).ToList();
            }
        }

        public bool DeletePics(List<int> list)
        {
            using (var context=new ResortManagementDbContext())
            {
                foreach (var item in GetPituresByPictureID(list))
                {
                    context.picture.Attach(item);
                    context.picture.Remove(item);
                }
                return context.SaveChanges()>0;
            }
        }
    }
}
