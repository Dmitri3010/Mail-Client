﻿using AutoMapper;
using MailClient.Imap.Interfaces;
using MailClient.Imap.Models;
using S22.Imap;
using System.Collections.Generic;
using System.Linq;

namespace MailClient.Imap.Services {
    public class ImapClientService : IClientService {
        private readonly IMapper _mapper;

        private readonly ImapAuthService _authService;
        private readonly List<MailService> _mailServices;

        private MailService _currentMailService;

        public ImapClientService(IMapper mapper) {
            _mapper = mapper;
            _authService = new ImapAuthService();
            _mailServices = new List<MailService>();
        }

        private MailService CreateMailServiceAndAddItToMailServiceList(string username) {
            var user = _authService.GetApplicationUserProfile(username);
            var mailService = new MailService(_mapper, user);
            _mailServices.Add(mailService);
            return mailService;
        }

        public ImapUser User => _currentMailService.User;

        public bool IsAnyUserLoggedIn() {
            return _authService.IsAnyUserLoggedIn();
        }

        public List<string> GetUsernamesOfAllUsersLoggedIn() {
            return _authService.GetUsernamesOfAllUsersLoggedIn();
        }

        public ImapUser GetApplicationUserProfile(string username) {
            return _authService.GetApplicationUserProfile(username);
        }

        public void Login(string hostname, string username, string password) {
            _authService.Login(hostname, username, password);
        }

        public void Logout(string username) {
            var mailService = _mailServices.FirstOrDefault(ms => ms.User.Username.Equals(username));
            if (mailService != null) {
                _mailServices.Remove(mailService);
            }
            _authService.Logout(username);
        }

        public void SwitchAccount(string username) {
            var mailService = _mailServices.FirstOrDefault(ms => ms.User.Username.Equals(username));
            if (mailService == null) {
                mailService = CreateMailServiceAndAddItToMailServiceList(username);
            }
            _currentMailService = mailService;
        }

        public List<MailModel> GetMessages(SearchCondition searchCondition) {
            return _currentMailService.GetMessages(searchCondition);
        }

        public List<MailModel> GetAllMessages() {
            return _currentMailService.GetAllMessages();
        }

        public List<MailModel> GetNewMessages() {
            return _currentMailService.GetNewMessages();
        }

        public List<MailModel> GetSentMessages() {
            return _currentMailService.GetSentMessages();
        }

        public List<MailModel> GetDeletedMessages() {
            return _currentMailService.GetDeletedMessages();
        }

        public List<MailModel> GetRecentMessages() {
            return _currentMailService.GetRecentMessages();
        }

        public List<MailModel> GetUnseenMessages() {
            return _currentMailService.GetUnseenMessages();
        }

        public List<MailModel> GetSpamMessages() {
            return _currentMailService.GetSpamMessages();
        }
    }
}
