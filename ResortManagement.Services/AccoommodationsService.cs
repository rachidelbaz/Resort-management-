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
                var Accommodations=context.accommodation.Include(acc=>acc.accommodationPictures).Include(acc=>acc.accommodationGatgets).AsQueryable();

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

        public IEnumerable<Accommodations> GetAccommodationsByBooking(DateTime? checkIn, int Duration, int? noOfBeds, int pageNo, int pageSize)
        {
            using (var context = new ResortManagementDbContext())
            {
                var k = context.booking.Select(b => b.AccommmodationDate).ToList();
                var accommodations= context.accommodation.Include(acc => acc.accommodationPictures).Include(acc=>acc.accommodationGatgets).AsQueryable();
                if (checkIn.HasValue && Duration>0)
                {
                    
                    accommodations = accommodations.Where(acc=>!context.booking.Select(b=>b.AccommodationID).ToList().Contains(acc.ID)||context.booking.Where(b=>b.AccommmodationDate.AddDays(b.Duration)<=checkIn).Select(b=>b.AccommodationID).ToList().Contains(acc.ID));
                }
                if (noOfBeds.HasValue)
                {
                    accommodations = accommodations.Where(acc=>acc.accommodationGatgets.NOFBeds==noOfBeds);
                }
                return accommodations.OrderByDescending(acc=>acc.ID).Skip((pageNo-1)* pageSize).Take(pageSize).ToList();
            }
            
        }

        public IEnumerable<Accommodations> GetAllAccommodations()
        {
            using (var context = new ResortManagementDbContext())
            {
                return context.accommodation.Include(acc=>acc.accommodationGatgets).ToList();
            }
        }

        public bool EditAccommodation(Accommodations NewModel,string name)
        {
            using (var context = new ResortManagementDbContext())
            {
                var oldModel = context.accommodation.Find(NewModel.ID);
                try
                {
                    NewModel.Name = null;
                    context.Entry(oldModel).CurrentValues.SetValues(NewModel);
                    NewModel.Name = name;
                    context.Entry(oldModel).CurrentValues.SetValues(NewModel);
                    if (NewModel.accommodationPictures != null)
                    {
                        foreach (var item in NewModel.accommodationPictures)
                        {
                            context.Entry(item).State = EntityState.Added;
                        }
                    }
                    return context.SaveChanges() > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool CreatAccommodation(Accommodations model)
        {
            using (var context= new ResortManagementDbContext())
            {
                try
                {
                    context.accommodation.Add(model);
                    return context.SaveChanges() > 0;
                }
                catch
                {
                    return false;
                }
               
            }
        }

       

        public Accommodations GetAccommodationsByID(int ID)
        {
            using (var context=new ResortManagementDbContext())
            {
                //return context.accommodation.Find(ID);
                return context.accommodation.Include(acc=>acc.accommodationPictures).FirstOrDefault(acc=>acc.ID==ID);
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
