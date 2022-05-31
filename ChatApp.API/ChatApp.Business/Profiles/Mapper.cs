using AutoMapper;
using ChatApp.Business.DTO_s.Autheticate;
using ChatApp.Business.DTO_s.Message;
using ChatApp.Business.DTO_s.User;
using ChatApp.Core.Entities;
using System.Collections.Generic;

namespace ChatApp.Business.Profiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, GetUserInfo>();
            CreateMap<User, GetUser>();
            CreateMap<Register, User>();
            CreateMap<MessageDto, Message>();
        }
    }
}
