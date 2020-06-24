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

        public IEnumerable<Bookings> GetSearchBookings(string searchTerm, int pageNo, int pageSize)
        {
            using (var context = new ResortManagementDbContext())
            {
                var bookings = context.booking.Include(b=>b.accommodation).Include(b=>b.RMUser).AsQueryable();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    bookings = context.booking.Where(b=>b.RMUser.FullName.ToLower().Contains(searchTerm.Trim().ToLower())|| b.RMUser.CIN.ToLower().Contains(searchTerm.Trim().ToLower()));
                }
                return bookings.OrderByDescending(b => b.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int GetSearchBookingsCount(string searchTerm)
        {
            using (var context = new ResortManagementDbContext())
            {
                var bookings = context.booking.Include(b => b.accommodation).Include(b => b.RMUser).AsQueryable();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    bookings = context.booking.Where(b => b.RMUser.FullName.ToLower().Contains(searchTerm.Trim().ToLower()) || b.RMUser.CIN.ToLower().Contains(searchTerm.Trim().ToLower()));
                }
                return bookings.ToList().Count();
            }
        }

        public Bookings GetBookingsByID(int value)
        {
            using (var Context =new ResortManagementDbContext())
            {
                return Context.booking.Include(b=>b.accommodation).FirstOrDefault(b=>b.ID==value);
            }
        }
    }
}
