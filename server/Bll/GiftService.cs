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

        public Gift Add(GiftDTO gifted)
        {
            var donor = donorDal.GetById(gifted.DonorId);
            if (donor == null)
                throw new Exception("Donor not found");

            var gift = mapper.Map<Gift>(gifted);
            gift.Donor = donor;
            return giftDal.Add(gift);
        }

        public bool Remove(int id)
        {
            return giftDal.Remove(id);
        }

        public void Update(int id, GiftDTO updateGift)
        {
            giftDal.Update(id, updateGift);
        }

        public Gift? GetByName(string name)
        {
            return giftDal.GetByName(name);
        }

        public List<Gift>? GetByDonorName(string firstName, string lastName)
        {
            return giftDal.GetByDonorName(firstName, lastName);
        }

        public List<Gift>? GetByBuyersNumber(int number)
        {
            return giftDal.GetByBuyersNumber(number);
        }

        public Gift? GetById(int id)
        {
            return giftDal.GetById(id);
        }

        public List<Gift> Get()
        {
            return giftDal.Get();
        }

        public List<Gift> GetByCategory(string category)
        {
            return giftDal.GetByCategory(category);
        }
    }
}
