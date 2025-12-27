using AutoMapper;
using server.Bll.Interfaces;
using server.Dal.Interfaces;
using server.Models;
using server.Models.DTO;

namespace server.Bll
{
    public class GiftService : IGiftService
    {
        private readonly IGiftDal giftDal;
        private readonly IDonorDal donorDal;
        private readonly IMapper mapper;

        public GiftService(IGiftDal giftDal, IDonorDal donorDal, IMapper mapper)
        {
            this.giftDal = giftDal;
            this.donorDal = donorDal;
            this.mapper = mapper;
        }

        public async Task<Gift> Add(GiftDTO gifted)
        {
            var gift = mapper.Map<Gift>(gifted);
            var donor = await donorDal.GetById(gifted.DonorId);
            if (donor == null)
                throw new Exception("Donor not found");

            gift.Donor = donor;
            return await giftDal.Add(gift);
        }

        public async Task<List<Gift>> Get()
        {
            return await giftDal.Get();
        }

        public async Task<List<Gift>?> GetByBuyersNumber(int number)
        {
            return await giftDal.GetByBuyersNumber(number);
        }

        public async Task<List<Gift>> GetByCategory(string category)
        {
            return await giftDal.GetByCategory(category);
        }

        public async Task<List<Gift>?> GetByDonorName(string firstName, string lastName)
        {
            return await giftDal.GetByDonorName(firstName, lastName);
        }

        public async Task<Gift?> GetById(int id)
        {
            return await giftDal.GetById(id);
        }

        public async Task<Gift?> GetByName(string firstname)
        {
            return await giftDal.GetByName(firstname);
        }

        public async Task<List<Gift>> GetByPrice(bool ascending = true)
        {
            return await giftDal.GetByPrice(ascending);
        }

        public async Task<bool> Remove(int id)
        {
            return await giftDal.Remove(id);
        }

        public async Task Update(int id, GiftDTO updateGift)
        {
            await giftDal.Update(id, updateGift);
        }
    }
}
