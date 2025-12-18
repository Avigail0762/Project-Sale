using AutoMapper;
using server.Bll.Interfaces;
using server.Dal;
using server.Dal.Interfaces;
using server.Models;
using server.Models.DTO;

namespace server.Bll
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerDal CustomerDal;
        private readonly IGiftService _giftService;
        private readonly IMapper _mapper;

        public CustomerService(
            CustomerDal customerDal,
            IGiftService giftService,
            IMapper mapper)
        {
            CustomerDal = customerDal;
            _giftService = giftService;
            _mapper = mapper;
        }

        // ---------- AUTH ----------
        public User Register(UserDTO dto)
        {
            if (CustomerDal.GetUserByEmail(dto.Email) != null)
                throw new Exception("Email already exists");

            var user = _mapper.Map<User>(dto);
            return CustomerDal.AddUser(user);
        }

        public User Login(string email)
        {
            var user = CustomerDal.GetUserByEmail(email);
            if (user == null)
                throw new Exception("User not found");

            return user;
        }

        // ---------- GIFTS ----------
        public List<Gift> GetGifts(string? category, bool? sortPriceAsc)
        {
            if (category != null)
                return _giftService.GetByCategory(category);

            if (sortPriceAsc != null)
                return _giftService.GetByPrice(sortPriceAsc.Value);

            return _giftService.Get();
        }

        // ---------- CART ----------
        public void AddToCart(int userId, int giftId)
        {
            var user = CustomerDal.GetUserById(userId)
                ?? throw new Exception("User not found");

            user.ShoppingCart.Add(giftId);
            CustomerDal.UpdateUser(user);
        }

        public void RemoveFromCart(int userId, int giftId)
        {
            var user = CustomerDal.GetUserById(userId)
                ?? throw new Exception("User not found");

            user.ShoppingCart.Remove(giftId);
            CustomerDal.UpdateUser(user);
        }

        // ---------- PURCHASE ----------
        public void Purchase(int userId)
        {
            var user = CustomerDal.GetUserById(userId)
                ?? throw new Exception("User not found");

            foreach (var giftId in user.ShoppingCart)
            {
                var ticket = new Ticket
                {
                    UserId = userId,
                    GiftId = giftId,
                };

                CustomerDal.AddTicket(ticket);
            }

            user.ShoppingCart.Clear();
            CustomerDal.UpdateUser(user);
        }

    }

}
