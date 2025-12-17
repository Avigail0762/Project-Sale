using server.Dal.Interfaces;
using server.Models;
using server.Models.DTO;

namespace server.Dal
{
    public class DonorDal : IDonorDal
    {
        private readonly SaleContext saleContext;

        public DonorDal (SaleContext saleContext)
        {
            this.saleContext = saleContext;
        }

        public Donor Add(Donor donor)
        {
            if (donor == null)
                throw new ArgumentNullException(nameof(donor));

            saleContext.Donors.Add(donor);
            saleContext.SaveChanges();
            return donor;
        }

        public bool Remove(int id)
        {
            var donor = saleContext.Donors.Find(id);
            if (donor == null)
                return false;

            saleContext.Donors.Remove(donor);
            saleContext.SaveChanges();
            return true;
        }

        public void Update(int id, DonorDTO updateDonor)
        {
            var donor = saleContext.Donors.Find(id);
            if (donor == null)
                throw new Exception("Donor לא נמצא");

            // עדכון שדות
            donor.FirstName = updateDonor.FirstName;
            donor.LastName = updateDonor.LastName;
            donor.Email = updateDonor.Email;

            saleContext.SaveChanges();
        }

        public List<Donor> Get()
        {
            return saleContext.Donors.ToList();
        }

        public Donor? GetByEmail(string email)
        {
            return saleContext.Donors.FirstOrDefault(d => d.Email == email);
        }

        public Donor? GetByGift(Gift gift)
        {
            return saleContext.Donors.FirstOrDefault(d => d.Gifts.Contains(gift));
        }

        public Donor? GetByName(string firstName, string lastName)
        {
            return saleContext.Donors
                .FirstOrDefault(d => d.FirstName == firstName && d.LastName == lastName);
        }

        public Donor? GetById(int id)
        {
            return saleContext.Donors.FirstOrDefault(d => d.Id == id);
        }
    }
}
