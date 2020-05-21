using ResortManagement.DataBase;
using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortManagement.Services
{
    public class AccoommodationsService
    {
        public static AccoommodationsService Instance 
        { get {
                if (instance==null)
                {
                    instance = new AccoommodationsService();
                }
                return instance;
            }
        }
        private static AccoommodationsService instance { get; set; }

        public IEnumerable<Accommodations> GetAllAccommodations(string searchTerm, string accommodationGatgets, int pageSize, int pageNo) 
        {
            using (var context=new ResortManagementDbContext())
            {
                var Accommodations=context.accommodation.Include(acc=>acc.accommodationGatgets).AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    Accommodations = Accommodations.Where(Acc => Acc.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
                }
                if(!string.IsNullOrEmpty(accommodationGatgets))
                {
                    Accommodations = Accommodations.Where(Acc => Acc.Name.ToLower().Contains(accommodationGatgets.Trim().ToLower()));
                }

                return Accommodations.Skip((pageNo-1)*pageSize).Take(pageSize).ToList();
                
            }
        }

        public int GetAllAccommodationsCount(string searchTerm, string accommodationGatgets)
        {
            using (var context = new ResortManagementDbContext())
            {
                var Accommodations = context.accommodation.Include(acc => acc.accommodationGatgets).AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    Accommodations = Accommodations.Where(Acc => Acc.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
                }
                if (!string.IsNullOrEmpty(accommodationGatgets))
                {
                    Accommodations = Accommodations.Where(Acc => Acc.Name.ToLower().Contains(accommodationGatgets.Trim().ToLower()));
                }

                return Accommodations.Count();

            }
        }

        public Accommodations GetAccommodationsByID(int ID)
        {
            using (var context=new ResortManagementDbContext())
            {
                return context.accommodation.Find(ID);
            }
        }
    }
}
