using Microsoft.EntityFrameworkCore;
using server.Dal.Interfaces;
using server.Models;

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
        public async Task<User?> GetUserByEmail(string email)
            => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetUserById(int id)
            => await _context.Users.FindAsync(id);

        public async Task<User> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // ---------- TICKETS ----------
        public async Task AddTicket(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }
    }
}
