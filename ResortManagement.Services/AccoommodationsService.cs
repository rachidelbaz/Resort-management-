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

        public IEnumerable<Accommodations> GetAllAccommodations(string searchTerm, int accommodationGatget, int pageSize, int pageNo) 
        {
            using (var context=new ResortManagementDbContext())
            {
                var Accommodations=context.accommodation.Include(acc=>acc.accommodationGatgets).AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    Accommodations = Accommodations.Where(Acc => Acc.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
                }
                if(accommodationGatget>0)
                {
                    Accommodations = Accommodations.Where(Acc => Acc.AccommodationGatgetID==accommodationGatget);
                }

                return Accommodations.OrderByDescending(Acc=>Acc.ID).Skip((pageNo-1)*pageSize).Take(pageSize).ToList();
                
            }
        }

        public int GetAllAccommodationsCount(string searchTerm, int accommodationGatget)
        {
            using (var context = new ResortManagementDbContext())
            {
                var Accommodations = context.accommodation.Include(acc => acc.accommodationGatgets).AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    Accommodations = Accommodations.Where(Acc => Acc.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
                }
                if (accommodationGatget>0)
                {
                    Accommodations = Accommodations.Where(Acc => Acc.AccommodationGatgetID==accommodationGatget);
                }

                return Accommodations.Count();

            }
        }

        public IEnumerable<Accommodations> GetAllAccommodations()
        {
            using (var context = new ResortManagementDbContext())
            {
                return context.accommodation.ToList();
            }
        }

        public bool EditAccommodation(Accommodations model)
        {
            using (var context = new ResortManagementDbContext())
            {
                context.Entry(model).State = EntityState.Modified;
                return context.SaveChanges() > 0;
            }
        }

        public bool CreatAccommodation(Accommodations model)
        {
            using (var context= new ResortManagementDbContext())
            {
                context.accommodation.Add(model);
                return context.SaveChanges() > 0;
            }
        }

        public Accommodations GetAccommodationsByID(int ID)
        {
            using (var context=new ResortManagementDbContext())
            {
                return context.accommodation.Find(ID);
            }
        }

        public bool DeleteAccommodationByID(int value)
        {
            using (var context=new ResortManagementDbContext())
            {
                var model = GetAccommodationsByID(value);
                context.accommodation.Attach(model);
                context.accommodation.Remove(model);
                return context.SaveChanges() > 0;
            }
        }
    }
}
