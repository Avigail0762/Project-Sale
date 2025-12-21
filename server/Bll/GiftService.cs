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
            Console.WriteLine("Start Add");
            var donor = donorDal.GetById(gifted.DonorId);
            Console.WriteLine("Donor: " + (donor != null ? donor.Id.ToString() : "null"));

            if (donor == null)
                throw new Exception("Donor not found");


            var gift = new Gift
            {
                Name = gifted.Name,
                Description = gifted.Description,
                DonorId = gifted.DonorId,
                Price = gifted.Price,
                BuyersNumber = gifted.BuyersNumber,
                Category = gifted.Category,
                WinnerTicketId = gifted.WinnerTicketId,
                IsDrawn = gifted.IsDrawn
            };
            Console.WriteLine("Gift mapped: " + (gift != null ? gift.Name : "null"));
            //gift.DonorId = gifted.DonorId;

            try
            {
                var result = giftDal.Add(gift);
                Console.WriteLine("Gift added: " + (result != null ? result.Id.ToString() : "null"));
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }
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
        public List<Gift> GetByPrice(bool ascending = true)
        {
            return giftDal.GetByPrice(ascending);
        }

    }
}
