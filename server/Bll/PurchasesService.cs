using server.Bll.Interfaces;
using server.Dal.Interfaces;
using server.Models;

namespace server.Bll
{
    public class PurchasesService : IPurchasesService
    {
        private readonly IPurchasesDal purchasesDal;

        public PurchasesService(IPurchasesDal dal)
        {
            purchasesDal = dal;
        }

        public List<Ticket> GetTicketsByGiftId(int giftId)
        {
            return purchasesDal.GetTicketsByGiftId(giftId);
        }

        public List<Gift> GetGiftsSortedByPrice()
        {
            return purchasesDal.GetGiftsSortedByPrice();
        }

        public List<Gift> GetGiftsSortedByPurchases()
        {
            return purchasesDal.GetGiftsSortedByPurchases();
        }

        public List<User> GetBuyersByGiftId(int giftId)
        {
            return purchasesDal.GetBuyersByGiftId(giftId);
        }
    }

}
