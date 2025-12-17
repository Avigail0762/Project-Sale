using AutoMapper;
using server.Bll.Interfaces;
using server.Models;
using server.Models.DTO;

namespace server.Profiles
{
    public class GiftProfile : Profile
    {
        int index = 0;
        public GiftProfile()
        {

            CreateMap<GiftDTO, Gift>()
                .ForMember(x => x.Id,
                 y => y.MapFrom(s => index));
            index++;
        }
    }
}
