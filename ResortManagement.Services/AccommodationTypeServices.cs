using ResortManagement.DataBase;
using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortManagement.Services
{
    public class AccommodationTypeServices
    {
        private static AccommodationTypeServices instance { get; set; }
        public static AccommodationTypeServices Instance
        {
            get
            {
               if(instance==null)
                {
                    instance=new AccommodationTypeServices();
                }
                return instance;
            }
            
        }

        public IEnumerable<AccommodationTypes> GetAllAccommondationTypes()
        {
            using (var context =new ResortManagementDbContext())
            {
               return context.accommodationType.ToList();
            }
                
        }

        public bool CreateAccommondationType(AccommodationTypes model)
        {
            using (var context =new ResortManagementDbContext())
            {
                context.accommodationType.Add(model);
                return context.SaveChanges() > 0;
            }
                
        }

        public IEnumerable<AccommodationTypes> GetSearchAccommondationTypes(string searchTerm,int pageNo)
        {
            int pageSize = 5;
            using (var context=new ResortManagementDbContext())
            {
                var accommodationType = context.accommodationType.ToList();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    accommodationType= accommodationType.Where(acc => acc.Type.ToLower().Contains(searchTerm.ToLower().Trim())).ToList();
                }

                accommodationType.OrderByDescending(acc=>acc.ID).Skip(pageSize).Take((pageNo - 1) * pageSize);

                return accommodationType;
            }
           
        }

        public int GetSearchAccommondationTypesCount(string searchTerm)
        {
            using (var context= new ResortManagementDbContext())
            {
                var accommodationTypesSearch = context.accommodationType.ToList();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    accommodationTypesSearch = accommodationTypesSearch.Where(acc => acc.Type.ToLower().Contains(searchTerm.Trim().ToLower())).ToList();
                }
                return accommodationTypesSearch.Count();
            }
                
        }
    }
}
