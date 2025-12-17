using server.Models;
using server.Models.DTO;

namespace server.Bll.Interfaces
{
    public interface IDonorService
    {
        Donor Add(DonorDTO donored);
        bool Remove(int id);
        void Update(int id, DonorDTO updateDonor);
        Donor? GetByName(string firstName, string lastName);
        Donor? GetByEmail(string email);
        Donor? GetById(int id);
        Donor? GetByGift(Gift gift);
        List<Donor> Get();
    }
}
