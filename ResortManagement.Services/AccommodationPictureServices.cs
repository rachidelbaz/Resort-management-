using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortManagement.DataBase;
using ResortManagement.Entities;

namespace ResortManagement.Services
{
    public class AccommodationPictureServices
    {
        public static AccommodationPictureServices Instance
        {
            get
            {
                if (instance==null)
                {
                    instance = new AccommodationPictureServices();
                }
                return instance;
            }
        }
        private static AccommodationPictureServices instance {get;set;}

        public void CreateAccommodationPicture(AccommodationPicture newAccommodationPic)
        {
            using (var context= new ResortManagementDbContext())
            {
                
            }
        }
    }
}
