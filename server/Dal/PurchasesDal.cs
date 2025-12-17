using server.Dal.Interfaces;
using server.Models;

namespace server.Dal
{
    public class PurchasesDal : IPurchasesDal
    {
        private readonly SaleContext saleContext;

        public PurchasesDal(SaleContext context)
        {
            saleContext = context;
        }

        // כל הכרטיסים של מתנה מסוימת
        public List<Ticket> GetTicketsByGiftId(int giftId)
        {
            return saleContext.Tickets
                           .Where(t => t.GiftId == giftId)
                           .ToList();
        }

        // מיון לפי המתנה היקרה ביותר 
        public List<Gift> GetGiftsSortedByPrice()
        {
            return saleContext.Gifts
                           .OrderByDescending(g => g.Price)
                           .ToList();
        }

        // מיון לפי המתנה הנרכשת ביותר 
        public List<Gift> GetGiftsSortedByPurchases()
        {
            return saleContext.Gifts
                           .OrderByDescending(g => g.BuyersNumber)
                           .ToList();
        }

        // צפייה בפרטי הרוכשים עבור מתנה מסוימת 
        public List<User> GetBuyersByGiftId(int giftId)
        {
            // שליפת כל הכרטיסים של המתנה
            var tickets = saleContext.Tickets
                                  .Where(t => t.GiftId == giftId)
                                  .ToList();

            // יצירת רשימת יוזרים בלי כפילויות
            List<User> buyers = new List<User>();

            foreach (var t in tickets)
            {
                var user = saleContext.Users.FirstOrDefault(u => u.Id == t.UserId);
                if (user != null && !buyers.Any(b => b.Id == user.Id))
                {
                    buyers.Add(user);
                }
            }

            return buyers;
        }
    }
}
