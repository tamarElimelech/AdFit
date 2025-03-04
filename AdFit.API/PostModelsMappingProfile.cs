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
        }

    }
}
