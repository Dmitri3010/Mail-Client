using MailClient.Imap.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using MailClient.Imap.Models;
using MailClient.DAL.Interfaces;
using AutoMapper;
using MailClient.Imap.Services;
using S22.Imap;
using MailClient.DAL.Entities;

namespace MailClient.Imap.Proxy {
    public class ClientServiceProxy : IClientService {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _database;
        private readonly IClientService _clientService;

        public ClientServiceProxy(IMapper mapper, IUnitOfWork unitOfWork) {
            _mapper = mapper;
            _database = unitOfWork;
            _clientService = new ImapClientService(_mapper);
        }

        public ImapUser User => _clientService.User;

        private bool CheckForUpdateNeeded() {
            return _database.MessageRepository.Query().Count() == 0;
        }

        private User GetUserEntity(string hostname, string username) {
            return new User {
                Hostname = hostname,
                Username = username
            };
        }

        private void AddUserToDatabaseIfNeeded(string hostname, string username) {
            User user = _database.UserRepository.Query().FirstOrDefault(u => u.Hostname.Equals(hostname) && u.Username.Equals(username));
            if (user == null) {
                user = GetUserEntity(hostname, username);
                _database.UserRepository.Add(user);
            }
        } 

        private void AddMailToDatabaseIfNeeded(List<MailModel> mail) {
            var messages = _mapper.Map<List<MailModel>, List<Message>>(mail);
            var currentUserUsername = _clientService.User.Username;
            _database.MessageRepository.AddRangeWhichNotIncludedAndSetOwnerUserId(messages, currentUserUsername);
        }

        public List<MailModel> GetMessages(SearchCondition searchCondition) {
            return _clientService.GetMessages(searchCondition);
        }

        public List<MailModel> GetAllMessages() {
            if (CheckForUpdateNeeded()) {
                var messages = _database.MessageRepository.GetAllUserMessages(_clientService.User.Username).ToList();
                return _mapper.Map<List<Message>, List<MailModel>>(messages);
            }
            var mail = _clientService.GetAllMessages();
            AddMailToDatabaseIfNeeded(mail);
            return mail;
        }

        public ImapUser GetApplicationUserProfile(string username) {
            return _clientService.GetApplicationUserProfile(username);
        }

        public List<MailModel> GetDeletedMessages() {
            var mail = _clientService.GetDeletedMessages();
            AddMailToDatabaseIfNeeded(mail);
            return mail;
        }

        public List<MailModel> GetNewMessages() {
            var mail = _clientService.GetNewMessages();
            AddMailToDatabaseIfNeeded(mail);
            return mail;
        }

        public List<MailModel> GetRecentMessages() {
            var mail = _clientService.GetRecentMessages();
            AddMailToDatabaseIfNeeded(mail);
            return mail;
        }

        public List<MailModel> GetSpamMessages() {
            // TODO: get spam mail from S22.IMap by flag

            throw new NotImplementedException();
        }

        public List<MailModel> GetUnseenMessages() {
            var mail = _clientService.GetUnseenMessages();
            AddMailToDatabaseIfNeeded(mail);
            return mail;
        }

        public List<MailModel> GetSentMessages() {
            var mail = _clientService.GetSentMessages();
            AddMailToDatabaseIfNeeded(mail);
            return mail;
        }

        public bool IsAnyUserLoggedIn() {
            return _clientService.IsAnyUserLoggedIn();
        }

        public void Login(string hostname, string username, string password) {
            _clientService.Login(hostname, username, password);
            AddUserToDatabaseIfNeeded(hostname, username);
        }

        public void Logout(string username) {
            _clientService.Logout(username);
        }

        public void SwitchAccount(string username) {
            _clientService.SwitchAccount(username);
        }

        public List<string> GetUsernamesOfAllUsersLoggedIn() {
            return _clientService.GetUsernamesOfAllUsersLoggedIn();
        }
    }
}
