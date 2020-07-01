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
    public class BookingsServices
    {
        public static BookingsServices Instance
        {
            get {
                if (instance==null)
                {
                    instance = new BookingsServices();
                }
                return instance;
            }
        }
        private static BookingsServices instance
        {
            get;set;
        }

        public IEnumerable<Bookings> GetSearchBookings(string searchTerm,string Status, int pageNo, int pageSize)
        {
            using (var context = new ResortManagementDbContext())
            {
                var bookings = context.booking.Include(b=>b.accommodation).Include(b=>b.RMUser).AsQueryable();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    bookings = context.booking.Where(b=>b.RMUser.FullName.ToLower().Contains(searchTerm.Trim().ToLower())|| b.RMUser.CIN.ToLower().Contains(searchTerm.Trim().ToLower()));
                }
                if (!string.IsNullOrEmpty(Status))
                {
                    bookings = context.booking.Where(b => b.Status== Status);
                }
                return bookings.OrderByDescending(b => b.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int GetSearchBookingsCount(string searchTerm, string Status)
        {
            using (var context = new ResortManagementDbContext())
            {
                var bookings = context.booking.Include(b => b.accommodation).Include(b => b.RMUser).AsQueryable();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    bookings = context.booking.Where(b => b.RMUser.FullName.ToLower().Contains(searchTerm.Trim().ToLower()) || b.RMUser.CIN.ToLower().Contains(searchTerm.Trim().ToLower()));
                }
                if (!string.IsNullOrEmpty(Status))
                {
                    bookings = context.booking.Where(b => b.Status == Status);
                }
                return bookings.ToList().Count();
            }
        }

        public bool EditBookings(Bookings model)
        {
            using (var Context= new ResortManagementDbContext())
            {
                Context.Entry(model).State = EntityState.Modified;
                return Context.SaveChanges() > 0;
            }
        }

        public bool CreateBookings(Bookings model)
        {
            using (var Context= new ResortManagementDbContext())
            {
                Context.booking.Add(model);
                return Context.SaveChanges() > 0;
            }
        }

        public Bookings GetBookingsByID(int value)
        {
            using (var Context =new ResortManagementDbContext())
            {
                return Context.booking.Include(b=>b.accommodation).FirstOrDefault(b=>b.ID==value);
            }
        }

        public bool DeleteBookingsByID(int iD)
        {
            using (var Context= new ResortManagementDbContext())
            {
                var model=Context.booking.Find(iD);
                Context.booking.Attach(model);
                Context.booking.Remove(model);
                return Context.SaveChanges() > 0;
            }
        }

        public DateTime GetAccoAvailableDate(int value)
        {
            using (var Context =new ResortManagementDbContext())
            {
                return Context.booking.Where(b => b.AccommodationID == value).Select(b =>b.AccommmodationDate).FirstOrDefault().AddDays(Context.booking.Where(b=>b.AccommodationID==value).Select(b=>b.Duration).FirstOrDefault());
            }
        }
    }
}
