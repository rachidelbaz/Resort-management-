using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortManagement.DataBase;
using ResortManagement.Entities;

namespace ResortManagement.Services
{
    public class AccommodationGadgetsServices
    {
        public static AccommodationGadgetsServices Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccommodationGadgetsServices();
                }
                return instance;
            }
        }
        private static AccommodationGadgetsServices instance { get; set; }

        public IEnumerable<AccommodationGatgets> GetAllAccommodationGadgets(string searchTerm, int AccomoodationType, int pagSize, int pagNo)
        {
            using (var context = new ResortManagementDbContext())
            {
                var AccommodationGadgets = context.accommodationGatget.AsQueryable();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    AccommodationGadgets = AccommodationGadgets.Where(AccG => AccG.Name.Contains(searchTerm.ToLower().Trim()));
                }
                if(AccomoodationType>0)
                {
                    AccommodationGadgets = AccommodationGadgets.Where(AccG => AccG.AccommodationTypeID== AccomoodationType);
                }
                return AccommodationGadgets.OrderByDescending(AccG => AccG.ID).Skip((pagNo - 1) * pagSize).Take(pagSize).ToList();
            }
        }
        public IEnumerable<AccommodationGatgets> GetAllAccommodationGadgets()
        {
            using (var context = new ResortManagementDbContext())
            {          
               return context.accommodationGatget.Distinct().ToList();   
            }
        }

        public int GetAccommodationGadgetsCout(string searchTerm, int accomoodationType)
        {
            using (var context = new ResortManagementDbContext())
            {
                var AccommodationGadgets = context.accommodationGatget.AsQueryable();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    return AccommodationGadgets.Where(AccG => AccG.Name.Contains(searchTerm.ToLower().Trim())).ToList().Count();
                }
                if (accomoodationType > 0)
                {
                    AccommodationGadgets = AccommodationGadgets.Where(AccG => AccG.AccommodationTypeID == accomoodationType);
                }
                
               return AccommodationGadgets.ToList().Count();
                
    
            }
        }

        public AccommodationGatgets GetAccommodationGadgetsByID(int id)
        {
            using (var context = new ResortManagementDbContext())
            {
                //return context.accommodationGatget.Find(id);
                return context.accommodationGatget.Include(acc=>acc.GadgetPictures).FirstOrDefault(acc=>acc.ID==id);
            }
        }
        public IEnumerable<AccommodationGatgets> GetAccommodationGadgetsByIDs(List<int> ids)
        {
            using (var context = new ResortManagementDbContext())
            {
                return context.accommodationGatget.Where(acc=>ids.Contains(acc.ID)).ToList();

            }
        }

        public bool EditAccommondationGadget(AccommodationGatgets model)
        {
            using (var context = new ResortManagementDbContext())
            {
                var gadget = context.accommodationGatget.Find(model.ID);
                context.Entry(gadget).CurrentValues.SetValues(model);
                if (model.GadgetPictures!=null)
                {
                    foreach (var item in model.GadgetPictures)
                    {
                        context.Entry(item).State = EntityState.Added;
                    }
                }
               
                return context.SaveChanges() > 0;
            }
        }

        public bool CreateAccommondationGadget(AccommodationGatgets model)
        {
            using (var context = new ResortManagementDbContext())
            {
                 context.accommodationGatget.Add(model);
                return context.SaveChanges() > 0;
            }
        }
         public bool DeleteAccommondationGadget(int id)
        {
            using (var context = new ResortManagementDbContext())
            {
                var Model = GetAccommodationGadgetsByID(id);
                 context.accommodationGatget.Attach(Model);
                 context.accommodationGatget.Remove(Model);
                return context.SaveChanges() > 0;
            }
        }
        public IEnumerable<AccommodationGatgets> GetGAdgetByAccTypeID(int value)
        {
            using (var context = new ResortManagementDbContext())
            {
                return context.accommodationGatget.Where(acc => acc.AccommodationTypeID == value).ToList();
            }
        }
    }
}
