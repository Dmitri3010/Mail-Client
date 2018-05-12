using AutoMapper;
using MailClient.DAL.Entities;
using MailClient.DAL.Interfaces;
using MailClient.IMap.Models;
using MailClient.IMap.Services;
using MailClient.Proxy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailClient.Proxy.Repositories
{
    public class MessageRepositoryProxy : IMessageRepositoryProxy
    {
        private readonly AuthService _authService;
        private readonly List<MailService> _mailServices;

        private readonly IMapper _mapper;
        private readonly IMessageRepository _messageRepository;

        private MailService _currentMailService;

        public bool ForceUpdate { get; set; }

        public MessageRepositoryProxy(IMapper mapper, IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _authService = new AuthService();
            _mailServices = new List<MailService>();

            ForceUpdate = false;
        }

        private bool CheckForUpdateNeeded()
        {
            return _messageRepository.Query().Count() == 0
                || ForceUpdate;
        }

        private MailService CreateMailServiceAndAddItToMailServiceList(string username)
        {
            var user = _authService.GetApplicationUserProfile(username);
            var mailService = new MailService(_mapper, user);
            _mailServices.Add(mailService);
            return mailService;
        }

        public bool IsAnyUserLoggedIn()
        {
            return _authService.IsAnyUserLoggedIn();
        }

        public void Login(string hostname, string username, string password)
        {
            _authService.AddApplicationUserProfile(hostname, username, password);
        }

        public void Logout(string username)
        {
            var mailService = _mailServices.FirstOrDefault(ms => ms.User.Username.Equals(username));
            if (mailService != null)
            {
                _mailServices.Remove(mailService);
            }
            _authService.RemoveApplicationUserProfile(username);
        }

        public void SwitchAccount(string username)
        {
            var mailService = _mailServices.FirstOrDefault(ms => ms.User.Username.Equals(username));
            if (mailService == null)
            {
                mailService = CreateMailServiceAndAddItToMailServiceList(username);
            }
            _currentMailService = mailService;
        }

        public IEnumerable<Message> GetAll()
        {
            return _messageRepository.GetAll();
        }

        public IEnumerable<Message> GetAllMessages()
        {
            return GetAllUserMessages(_currentMailService.User.Username);
        }

        public IEnumerable<Message> GetNewMessages()
        {
            var mail = _currentMailService.GetNewMessages();
            var messages = AddRangeWhichNotIncludedAndSetOwnerUserId(_mapper.Map<List<MailModel>, List<Message>>(mail), _currentMailService.User.Username);
            return messages;
        }

        public IEnumerable<Message> GetSentMessages()
        {
            var mail = _currentMailService.GetSentMessages();
            var messages = _mapper.Map<List<MailModel>, List<Message>>(mail);
            AddRangeWhichNotIncludedAndSetOwnerUserId(messages, _currentMailService.User.Username);
            return messages;
        }

        public IEnumerable<Message> GetDeletedMessages()
        {
            var mail = _currentMailService.GetDeletedMessages();
            var messages = _mapper.Map<List<MailModel>, List<Message>>(mail);
            AddRangeWhichNotIncludedAndSetOwnerUserId(messages, _currentMailService.User.Username);
            return messages;
        }

        public IEnumerable<Message> GetRecentMessages()
        {
            var mail = _currentMailService.GetRecentMessages();
            var messages = _mapper.Map<List<MailModel>, List<Message>>(mail);
            AddRangeWhichNotIncludedAndSetOwnerUserId(messages, _currentMailService.User.Username);
            return messages;
        }

        public IEnumerable<Message> GetUnseenMessages()
        {
            var mail = _currentMailService.GetUnseenMessages();
            var messages = _mapper.Map<List<MailModel>, List<Message>>(mail);
            AddRangeWhichNotIncludedAndSetOwnerUserId(messages, _currentMailService.User.Username);
            return messages;
        }

        public IEnumerable<Message> GetSpamMessages()
        {
            var mail = _currentMailService.GetSpamMessages();
            var messages = _mapper.Map<List<MailModel>, List<Message>>(mail);
            AddRangeWhichNotIncludedAndSetOwnerUserId(messages, _currentMailService.User.Username);
            return messages;
        }

        public IEnumerable<Message> GetAllUserMessages(string username)
        {
            bool isUpdateNeeded = CheckForUpdateNeeded();

            if (isUpdateNeeded)
            {
                var mail = _currentMailService.GetAllMessages();
                _messageRepository.AddRangeWhichNotIncludedAndSetOwnerUserId(_mapper.Map<List<MailModel>, List<Message>>(mail), username);
            }

            return _messageRepository.GetAllUserMessages(username);
        }

        public Message Add(Message item)
        {
            return _messageRepository.Add(item);
        }

        public IEnumerable<Message> AddRange(IEnumerable<Message> items)
        {
            return _messageRepository.AddRange(items);
        }

        public IEnumerable<Message> AddRangeWhichNotIncluded(IEnumerable<Message> messages)
        {
            return _messageRepository.AddRangeWhichNotIncluded(messages);
        }

        public IEnumerable<Message> AddRangeWhichNotIncludedAndSetOwnerUserId(IEnumerable<Message> messages, string username)
        {
            return _messageRepository.AddRangeWhichNotIncludedAndSetOwnerUserId(messages, username);
        }

        public Message Delete(Guid id)
        {
            return _messageRepository.Delete(id);
        }

        public IEnumerable<Message> DeleteRange(IEnumerable<Message> items)
        {
            return _messageRepository.DeleteRange(items);
        }

        public IEnumerable<Message> Get(Func<Message, bool> predicate)
        {
            return _messageRepository.Get(predicate);
        }

        public Message GetById(Guid id)
        {
            return _messageRepository.GetById(id);
        }

        public IQueryable<Message> Query()
        {
            return _messageRepository.Query();
        }
    }
}
