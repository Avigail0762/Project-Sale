using AutoMapper;
using server.Models;
using server.Models.DTO;

namespace server.Profiles
{
    public class DonorProfile: Profile
    {
        int index = 0;
        public DonorProfile()
        {
            CreateMap<DonorDTO, Donor>()
                .ForMember(x => x.Id,
                y => y.MapFrom(s => index));
            index++;
        }
    }
}
