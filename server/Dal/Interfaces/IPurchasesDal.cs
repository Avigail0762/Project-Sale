using server.Models;

namespace server.Dal.Interfaces
{
    public interface IPurchasesDal
    {
        List<Ticket> GetTicketsByGiftId(int giftId);
        List<Gift> GetGiftsSortedByPrice();
        List<Gift> GetGiftsSortedByPurchases();
        List<User> GetBuyersByGiftId(int giftId);

    }
}
