using AutoMapper;
using server.Bll.Interfaces;
using server.Dal.Interfaces;
using server.Models;
using server.Models.DTO;

namespace server.Bll
{
    public class DonorService : IDonorService
    {
        private readonly IDonorDal donorDal;
        private readonly IMapper mapper;

        public DonorService(IDonorDal donorDal, IMapper mapper)
        {
            this.donorDal = donorDal;
            this.mapper = mapper;
        }

        public Donor Add(DonorDTO donored)
        {
            var donor = mapper.Map<Donor>(donored);
            donorDal.Add(donor);
            return donor;
        }

        public List<Donor> Get()
        {
            return donorDal.Get();
        }

        public Donor? GetByEmail(string email)
        {
            return donorDal.GetByEmail(email);
        }

        public Donor? GetByGift(Gift gift)
        {
            return donorDal.GetByGift(gift);
        }

        public Donor? GetById(int id)
        {
            return donorDal.GetById(id);
        }

        public Donor? GetByName(string firstName, string lastName)
        {
            return donorDal.GetByName(firstName, lastName);
        }

        public bool Remove(int id)
        {
            donorDal.Remove(id);
            return true;
        }

        public void Update(int id, DonorDTO updateDonor)
        {
            donorDal.Update(id, updateDonor);
        }
    }
}