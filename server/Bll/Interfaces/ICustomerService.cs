using server.Models;
using server.Models.DTO;

namespace server.Bll.Interfaces
{
    public interface ICustomerService
    {
        // ---------- AUTH ----------
        User Register(UserDTO dto);
        User Login(string email);

        // ---------- GIFTS ----------
        List<Gift> GetGifts(string? category, bool? sortPriceAsc);

        // ---------- CART ----------
        void AddToCart(int userId, int giftId);
        void RemoveFromCart(int userId, int giftId);

        // ---------- PURCHASE ----------
        void Purchase(int userId);

    }
}
