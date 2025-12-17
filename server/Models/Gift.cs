namespace server.Models
{
    public class Gift
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int DonorId { get; set; }
        public Donor Donor { get; set; }
        public int Price { get; set; }
        public int BuyersNumber { get; set; }
        public string Category { get; set; }
    }
}
