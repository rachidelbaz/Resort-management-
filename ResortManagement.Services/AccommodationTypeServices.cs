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

        public IEnumerable<AccommodationTypes> GetSearchAccommondationTypes(string searchTerm,int pageNo, int pageSize)
        {
           
            using (var context=new ResortManagementDbContext())
            {
                
                var accommodationType = context.accommodationType.ToList();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    
                    accommodationType = accommodationType.Where(acc => !string.IsNullOrEmpty(acc.Type) && acc.Type.ToLower().Contains(searchTerm.ToLower().Trim())).ToList();
                }

                return accommodationType.OrderByDescending(acc=>acc.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                 
            }
           
        }

        public bool EditAccommondationType(AccommodationTypes model)
        {
            using (var context=new ResortManagementDbContext())
            {
                context.Entry(model).State = System.Data.Entity.EntityState.Modified;

                return context.SaveChanges()>0;
            }
        }

        public int GetSearchAccommondationTypesCount(string searchTerm)
        {
            using (var context= new ResortManagementDbContext())
            {
                var accommodationTypesSearch = context.accommodationType.ToList();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    accommodationTypesSearch = accommodationTypesSearch.Where(acc => !string.IsNullOrEmpty(acc.Type) && acc.Type.ToLower().Contains(searchTerm.Trim().ToLower())).ToList();
                }
                return accommodationTypesSearch.Count();
            }
                
        }

        public AccommodationTypes GetAccommondationTypeByID(int iD)
        {
            using (var context= new ResortManagementDbContext())
            {
                return context.accommodationType.Find(iD);

            }
        }

        public bool DeleteAccommondationTypeByID(int iD)
        {
            var model = GetAccommondationTypeByID(iD);
            using (var context = new ResortManagementDbContext())
            {
                context.accommodationType.Attach(model);
                 context.accommodationType.Remove(model);
                return context.SaveChanges()>0;
            }
        }
    }
}
