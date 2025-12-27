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

        public async Task<Donor> Add(DonorDTO donored)
        {
            var donor = mapper.Map<Donor>(donored);
            await donorDal.Add(donor);
            return donor;
        }

        public async Task<List<Donor>> Get()
        {
            return await donorDal.Get();
        }

        public async Task<Donor?> GetByEmail(string email)
        {
            return await donorDal.GetByEmail(email);
        }

        public async Task<Donor?> GetByGift(Gift gift)
        {
            return await donorDal.GetByGift(gift);
        }

        public async Task<Donor?> GetById(int id)
        {
            return await donorDal.GetById(id);
        }

        public async Task<Donor?> GetByName(string firstName, string lastName)
        {
            return await donorDal.GetByName(firstName, lastName);
        }

        public async Task<bool> Remove(int id)
        {
            await donorDal.Remove(id);
            return true;
        }

        public async Task Update(int id, DonorDTO updateDonor)
        {
            await donorDal.Update(id, updateDonor);
        }
    }
}