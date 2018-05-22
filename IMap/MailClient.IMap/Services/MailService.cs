using AutoMapper;
using MailClient.Imap.Interfaces;
using MailClient.Imap.Models;
using S22.Imap;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace MailClient.Imap.Services
{
    public class MailService : IMailService
    {
        private readonly IMapper _mapper;
        private readonly ImapUser _user;

        public ImapUser User => _user;

        public MailService(IMapper mapper, ImapUser user)
        {
            _user = user;
            _mapper = mapper;
        }

        public List<MailModel> GetMessages(SearchCondition searchCondition)
        {
            var uids = _user.Client.Search(searchCondition);
            var messages = _user.Client.GetMessages(uids);
            return _mapper.Map<List<MailMessage>, List<MailModel>>(messages.ToList());
        }

        public List<MailModel> GetNewMessages()
        {
            return GetMessages(SearchCondition.New())
                .OrderByDescending(m => m.Date)
                .ToList();
        }

        public List<MailModel> GetDeletedMessages()
        {
            return GetMessages(SearchCondition.Deleted())
                .OrderByDescending(m => m.Date)
                .ToList();
        }

        public List<MailModel> GetSpamMessages()
        {
            return GetMessages(SearchCondition.Draft())
                .OrderByDescending(m => m.Date)
                .ToList();
        }

        public List<MailModel> GetSentMessages()
        {
            return GetMessages(SearchCondition.From(_user.Username))
                .OrderByDescending(m => m.Date)
                .ToList();
        }

        public List<MailModel> GetRecentMessages()
        {
            return GetMessages(SearchCondition.Recent())
                .OrderByDescending(m => m.Date)
                .ToList();
        }

        public List<MailModel> GetUnseenMessages()
        {
            return GetMessages(SearchCondition.Unseen())
                .OrderByDescending(m => m.Date)
                .ToList();
        }

        public List<MailModel> GetAllMessages()
        {
            return GetMessages(SearchCondition.All())
                .OrderByDescending(m => m.Date)
                .ToList();
        }
    }
}
