using AutoMapper;
using server.Bll.Interfaces;
using server.Dal.Interfaces;
using server.Models;
using server.Models.DTO;

namespace server.Bll
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        private readonly IGiftService _giftService;
        private readonly IMapper _mapper;

        public CustomerService(
            ICustomerDal customerDal,
            IGiftService giftService,
            IMapper mapper)
        {
            _customerDal = customerDal;
            _giftService = giftService;
            _mapper = mapper;
        }

        public async Task<User> Register(UserDTO dto)
        {
            if (await _customerDal.GetUserByEmail(dto.Email) != null)
                throw new Exception("Email already exists");

            var user = _mapper.Map<User>(dto);
            return await _customerDal.AddUser(user);
        }

        public async Task<User?> Login(string email)
        {
            var user = await _customerDal.GetUserByEmail(email);
            if (user == null)
                throw new Exception("User not found");

            return user;
        }

        public async Task<List<Gift>> GetGifts(string? category, bool? sortPriceAsc)
        {
            if (category != null)
                return await _giftService.GetByCategory(category);

            if (sortPriceAsc != null)
                return await _giftService.GetByPrice(sortPriceAsc.Value);

            return await _giftService.Get();
        }

        public async Task AddToCart(int userId, int giftId)
        {
            var user = await _customerDal.GetUserById(userId);
            if (user == null) throw new Exception("User not found");

            var gift = await _giftService.GetById(giftId);
            if (gift == null) throw new Exception("Gift not found");

            if (gift.IsDrawn == true)
                throw new Exception("Cannot add drawn gift to cart");

            if (user.ShoppingCart == null) user.ShoppingCart = new List<int>();

            user.ShoppingCart.Add(giftId);
            await _customerDal.UpdateUser(user);
        }

        public async Task RemoveFromCart(int userId, int giftId)
        {
            var user = await _customerDal.GetUserById(userId);
            if (user == null) throw new Exception("User not found");

            if (user.ShoppingCart != null)
            {
                user.ShoppingCart.Remove(giftId);
                await _customerDal.UpdateUser(user);
            }
        }

        public async Task Purchase(int userId)
        {
            var user = await _customerDal.GetUserById(userId);
            if (user == null) throw new Exception("User not found");

            if (user.ShoppingCart == null || !user.ShoppingCart.Any()) return;

            var cartItems = user.ShoppingCart.ToList();

            foreach (var giftId in cartItems)
            {
                var gift = await _giftService.GetById(giftId);
                if (gift == null) continue;

                var ticket = new Ticket
                {
                    UserId = userId,
                    GiftId = giftId,
                    TicketNumberForGift = gift.BuyersNumber
                };

                await _customerDal.AddTicket(ticket);
                gift.BuyersNumber += 1;
            }

            user.ShoppingCart.Clear();
            await _customerDal.UpdateUser(user);
        }
    }
}
