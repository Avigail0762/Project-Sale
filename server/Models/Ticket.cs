namespace server.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int GiftId { get; set; }
        public Gift Gift { get; set; }
        public int TicketNumberForGift { get; set; }
        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
    }
}
