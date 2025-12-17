using AutoMapper;
using server.Models;
using server.Models.DTO;

namespace server.Profiles
{
    public class UserProfile : Profile
    {
        int index = 0;
        public UserProfile()
        {

            CreateMap<GiftDTO, Gift>()
                .ForMember(x => x.Id,
                 y => y.MapFrom(s => index));
            index++;
        }

    }
}
