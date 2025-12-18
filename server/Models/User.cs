namespace server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<int> ShoppingCart { get; set; } = new();
        public string Roles { get; set; } = "user";
    }

}
