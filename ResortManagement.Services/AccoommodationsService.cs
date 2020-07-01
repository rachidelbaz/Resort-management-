using ResortManagement.DataBase;
using ResortManagement.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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

        public IEnumerable<Accommodations> GetAccommodationsByBooking(string search, DateTime? checkIn, int Duration, int? noOfBeds, int pageNo, int pageSize)
        {
            using (var context = new ResortManagementDbContext())
            {
                var accommodations= context.accommodation.Include(acc => acc.accommodationPictures).Include(acc=>acc.accommodationGatgets).AsQueryable();
                if (checkIn.HasValue && Duration>0)
                {
                    var AccoIDsNotBookgins = context.booking.Where(b =>checkIn<=b.AccommmodationDate && b.AccommmodationDate<=EntityFunctions.AddDays(checkIn,Duration) ).Select(b => b.AccommodationID).ToList();
                    accommodations = accommodations.Where(acc=>!AccoIDsNotBookgins.Contains(acc.ID));
                }
                if (noOfBeds.HasValue)
                {
                    accommodations = accommodations.Where(acc=>(acc.accommodationGatgets.NOFBeds*acc.accommodationGatgets.NOfRoom)>=noOfBeds);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    var AccoTypeIDs = context.accommodationType.Where(accType=>accType.Type.ToLower().Contains(search.Trim().ToLower())).Select(accT=>accT.ID).ToList();
                    accommodations = accommodations.Where(acc => acc.accommodationGatgets.Name.ToLower().Contains(search.Trim().ToLower())|| AccoTypeIDs.Contains(acc.accommodationGatgets.AccommodationTypeID));
                }
                return accommodations.OrderByDescending(acc=>acc.ID).Skip((pageNo-1)* pageSize).Take(pageSize).ToList();
            }
            
        }
        public int GetAccommodationsByBookingCount(string search, DateTime? checkIn, int Duration, int? noOfBeds)
        {
            using (var context = new ResortManagementDbContext())
            {
                var accommodations = context.accommodation.Include(acc => acc.accommodationPictures).Include(acc => acc.accommodationGatgets).AsQueryable();
                if (checkIn.HasValue && Duration > 0)
                {
                    var AccoIDsNotBookgins = context.booking.Where(b => checkIn <= b.AccommmodationDate && b.AccommmodationDate <= EntityFunctions.AddDays(checkIn, Duration)).Select(b => b.AccommodationID).ToList();
                    accommodations = accommodations.Where(acc => !AccoIDsNotBookgins.Contains(acc.ID));
                }
                if (noOfBeds.HasValue)
                {
                    accommodations = accommodations.Where(acc => (acc.accommodationGatgets.NOFBeds * acc.accommodationGatgets.NOfRoom) >= noOfBeds);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    accommodations = accommodations.Where(acc=>acc.accommodationGatgets.Name.ToLower().Contains(search.Trim().ToLower()));
                }
                return accommodations.ToList().Count();
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

        public IEnumerable<Accommodations> GetAccByGadgetId(int value)
        {
            using (var context=new ResortManagementDbContext())
            {
                return context.accommodation.Where(acc=>acc.AccommodationGatgetID==value).ToList();
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
