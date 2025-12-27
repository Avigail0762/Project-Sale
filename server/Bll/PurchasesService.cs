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

        public async Task<List<Ticket>> GetTicketsByGiftId(int giftId)
        {
            return await purchasesDal.GetTicketsByGiftId(giftId);
        }

        public async Task<List<Gift>> GetGiftsSortedByPrice()
        {
            return await purchasesDal.GetGiftsSortedByPrice();
        }

        public async Task<List<Gift>> GetGiftsSortedByPurchases()
        {
            return await purchasesDal.GetGiftsSortedByPurchases();
        }

        public async Task<List<User>> GetBuyersByGiftId(int giftId)
        {
            return await purchasesDal.GetBuyersByGiftId(giftId);
        }
    }
}
