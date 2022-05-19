using AutoMapper;
using ChatApp.Business.DTO_s.Autheticate;
using ChatApp.Core.Entities;

namespace ChatApp.Business.Profiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Register, User>();
        }
    }
}
