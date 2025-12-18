using Microsoft.EntityFrameworkCore;
using server.Dal.Interfaces;
using server.Models;
using server.Models.DTO;

namespace server.Dal
{
    public class GiftDal : IGiftDal
    {
        private readonly SaleContext saleContext;
        private readonly IDonorDal donorDal;

        public GiftDal(SaleContext saleContext, IDonorDal donorDal)
        {
            this.saleContext = saleContext;
            this.donorDal = donorDal;
        }

        public Gift Add(Gift gift)
        {
            saleContext.Gifts.Add(gift);
            saleContext.SaveChanges();
            return gift;
        }
        public List<Gift> Get()
        {
            return saleContext.Gifts
                           .Include(g => g.Donor)
                           .ToList();
        }
        public List<Gift> GetByCategory(string category)
        {
            return saleContext.Gifts
                           .Include(g => g.Donor)
                           .Where(g => g.Category == category)
                           .ToList();
        }
        public List<Gift> GetByDonorName(string firstName, string lastName)
        {
            return saleContext.Gifts
                              .Include(g => g.Donor)
                              .Where(g => g.Donor.FirstName == firstName &&
                                          g.Donor.LastName == lastName)
                              .ToList();
        }

        public List<Gift> GetByBuyersNumber(int number)
        {
            return saleContext.Gifts
                              .Include(g => g.Donor)
                              .Where(g => g.BuyersNumber == number)
                              .ToList();
        }
        public Gift? GetById(int id)
        {
            return saleContext.Gifts
                           .Include(g => g.Donor)
                           .FirstOrDefault(g => g.Id == id);
        }
        public Gift? GetByName(string name)
        {
            return saleContext.Gifts
                           .Include(g => g.Donor)
                           .FirstOrDefault(g => g.Name == name);
        }
        public bool Remove(int id)
        {
            var gift = saleContext.Gifts.FirstOrDefault(g => g.Id == id);
            if (gift == null)
                return false;

            saleContext.Gifts.Remove(gift);
            saleContext.SaveChanges();
            return true;
        }
        public void Update(int id, GiftDTO updateGift)
        {
            var gift = saleContext.Gifts.FirstOrDefault(g => g.Id == id);
            if (gift == null)
                return;

            gift.Name = updateGift.Name;
            gift.Category = updateGift.Category;
            gift.Price = updateGift.Price;
            gift.BuyersNumber = updateGift.BuyersNumber;
            gift.DonorId = updateGift.DonorId;
            gift.Donor = donorDal.GetById(updateGift.DonorId);
            gift.WinnerTicketId = updateGift.WinnerTicketId;
            gift.IsDrawn = updateGift.IsDrawn;

            saleContext.SaveChanges();
        }
        public List<Gift> GetByPrice(bool ascending = true)
        {
            if (ascending)
            {
                return saleContext.Gifts
                    .OrderBy(g => g.Price)
                    .ToList();
            }
            else
            {
                return saleContext.Gifts
                    .OrderByDescending(g => g.Price)
                    .ToList();
            }
        }

    }
}
