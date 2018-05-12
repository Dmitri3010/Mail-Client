using AutoMapper;
using MailClient.DAL.Entities;
using MailClient.IMap.Models;
using MailClient.Models;
using System.Net.Mail;

namespace MailClient.Util
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MailMessage, MailModel>()
               .ForMember(message => message.Date, opt => opt.MapFrom(src => src.Headers["Date"]))
               .ReverseMap();

            CreateMap<MessageModel, ShortenMessageModel>()
                .ReverseMap();

            CreateMap<MessageModel, Message>()
                .ReverseMap();

            CreateMap<Message, MessageModel>()
                .ReverseMap();

            CreateMap<MailModel, Message>()
                .ReverseMap();

            CreateMap<Message, MailModel>()
                .ReverseMap();

            CreateMap<ApplicationUser, User>()
                .ReverseMap();

            CreateMap<User, ApplicationUser>()
                .ReverseMap();
        }
    }
}
