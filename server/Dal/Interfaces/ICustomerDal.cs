using server.Models;

namespace server.Dal.Interfaces
{
    public interface ICustomerDal
    {
        // ---------- USER ----------
        User? GetUserByEmail(string email);
        User? GetUserById(int id);
        User AddUser(User user);
        void UpdateUser(User user);

        // ---------- TICKETS ----------
        void AddTicket(Ticket ticket);

    }
}
