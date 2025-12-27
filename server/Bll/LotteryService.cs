using server.Bll.Interfaces;
using server.Dal.Interfaces;
using server.Models;

namespace server.Bll
{
    public class LotteryService : ILotteryService
    {
        private readonly IGiftDal _giftDal;
        private readonly IPurchasesDal _purchasesDal;
        private readonly IEmailService _emailService;

        public LotteryService(IGiftDal giftDal, IPurchasesDal purchasesDal, IEmailService emailService)
        {
            _giftDal = giftDal;
            _purchasesDal = purchasesDal;
            _emailService = emailService;
        }

        // מבצע הגרלה על מתנה אחת ומעדכן את Gift
        // אולי לשנות את זריקת השגיאות? שיהיה עם 
        // try & catch
        public async Task<Ticket> DoLottery(int giftId)
        {
            var gift = await _giftDal.GetById(giftId);
            if (gift == null)
                throw new Exception("Gift not found");
            if (gift.IsDrawn)
                throw new Exception("Lottery already done for this gift");

            var tickets = await _purchasesDal.GetTicketsByGiftId(giftId);
            if (tickets == null || tickets.Count == 0)
                throw new Exception("No tickets for this gift");

            var random = new Random();
            var winnerTicket = tickets[random.Next(tickets.Count)];

            // עדכון ה-Gift במסד
            gift.WinnerTicketId = winnerTicket.Id;
            gift.IsDrawn = true;

            //למה הפונ' עובדת ככה?????
            await _giftDal.Update(gift.Id, new server.Models.DTO.GiftDTO
            {
                Name = gift.Name,
                Category = gift.Category,
                Price = gift.Price,
                BuyersNumber = gift.BuyersNumber,
                DonorId = gift.DonorId,
                WinnerTicketId = gift.WinnerTicketId,
                IsDrawn = gift.IsDrawn
            });

            // שליחת מייל לזוכה
            var buyers = await _purchasesDal.GetBuyersByGiftId(gift.Id);
            var winnerUser = buyers.FirstOrDefault(u => u.Id == winnerTicket.UserId);
            
            if (winnerUser != null)
            {
                try
                {
                    await _emailService.SendWinnerEmailAsync(winnerUser.Email, gift.Name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                }
            }

            return winnerTicket;
        }

        // מחזיר את כל הכרטיסים המנצחים
        public async Task<List<Ticket>> GetWinnersReport()
        {
            var gifts = await _giftDal.Get();
            var winners = new List<Ticket>();

            foreach (var gift in gifts)
            {
                if (gift.IsDrawn && gift.WinnerTicketId.HasValue)
                {
                    var tickets = await _purchasesDal.GetTicketsByGiftId(gift.Id);
                    var ticket = tickets.FirstOrDefault(t => t.Id == gift.WinnerTicketId.Value);
                    if (ticket != null)
                        winners.Add(ticket);
                }
            }

            return winners;
        }

        // סך ההכנסות מכל הרכישות
        public async Task<decimal> GetTotalIncome()
        {
            var gifts = await _giftDal.Get();
            decimal total = 0;

            foreach (var gift in gifts)
            {
                total += gift.Price * gift.BuyersNumber;
            }

            return total;
        }
    }
}
