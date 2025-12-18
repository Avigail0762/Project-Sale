using server.Dal.Interfaces;
using server.Models;
using System;

namespace server.Dal
{
    public class CustomerDal : ICustomerDal
    {
        private readonly SaleContext _context;
        public CustomerDal(SaleContext context)
        {
            _context = context;
        }

        // ---------- USER ----------
        public User? GetUserByEmail(string email)
            => _context.Users.FirstOrDefault(u => u.Email == email);

        public User? GetUserById(int id)
            => _context.Users.Find(id);

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        // ---------- TICKETS ----------
        public void AddTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
        }

    }
}
