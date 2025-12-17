using server.Models;

namespace server.Bll.Interfaces
{
    public interface IPurchasesService
    {
        List<Ticket> GetTicketsByGiftId(int giftId);
        List<Gift> GetGiftsSortedByPrice();
        List<Gift> GetGiftsSortedByPurchases();
        List<User> GetBuyersByGiftId(int giftId);
    }
}
