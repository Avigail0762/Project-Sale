namespace server.Models.DTO
{
    public class GiftDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int DonorId { get; set; }
        public int Price { get; set; }
        public int BuyersNumber { get; set; }
        public string Category { get; set; }
    }
}
