using server.Models;
using server.Models.DTO;

namespace server.Bll.Interfaces
{
    public interface IGiftService
    {
        Gift Add(GiftDTO gifted);
        bool Remove(int id);
        void Update(int id, GiftDTO updateGift);
        Gift? GetByName(string firstname);
        List<Gift>? GetByDonorName(string firstName, string lastName);
        List<Gift>? GetByBuyersNumber(int number);
        Gift? GetById(int id);
        List<Gift> Get();
        List<Gift> GetByCategory(string category);
        List<Gift> GetByPrice(bool ascending = true);

    }
}
