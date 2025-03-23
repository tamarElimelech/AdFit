using AdFit.API.Models;
using AdFit.Core.Model;
using AutoMapper;

namespace AdFit.API
{
    public class PostModelsMappingProfile:Profile
    {
        public PostModelsMappingProfile()
        {
            CreateMap<UserPostModel, User>();
            CreateMap<AdvertisementPostModel, Advertisement>()
            .ForMember(dest => dest.User, opt => opt.Ignore());
            CreateMap<User,SignInModel>();
            CreateMap<PagePostModel, Page>();
            CreateMap<PricePostModel, Price>();
            CreateMap<AdminAdvertisementPostModel,AdminAdvertisement > ();
            CreateMap<AdminAdvertisement, Advertisement>()
           .ForMember(dest => dest.Id, opt => opt.Ignore()); 

        }

    }
}
