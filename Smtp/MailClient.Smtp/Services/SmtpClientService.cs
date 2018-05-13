using MailClient.Smtp.Interfaces;
using MailClient.Smtp.Models;
using System.Collections.Generic;
using System.Linq;
using MailClinet.Implementation.Models;

namespace MailClient.Smtp.Services {
    public class SmtpClientService : ISmtpClientService {
        private readonly ISmtpAuthService _authService;

        private SmtpUser _currentUser;

        public SmtpClientService() {
            _authService = new SmtpAuthService();
        }

        public SmtpUser User => _currentUser;

        public SmtpUser GetApplicationUserProfile(string username) {
            return _authService.GetApplicationUserProfile(username);
        }

        public List<string> GetUsernamesOfAllUsersLoggedIn() {
            return _authService.GetUsernamesOfAllUsersLoggedIn();
        }

        public bool IsAnyUserLoggedIn() {
            return _authService.IsAnyUserLoggedIn();
        }

        public void Login(string hostname, string username, string password) {
            _authService.Login(hostname, username, password);
            _currentUser = _authService.GetApplicationUserProfile(username);
        }

        public void Logout(string username) {
            if (_currentUser.Username.Equals(username)) {
                _currentUser = _authService.GetApplicationUserProfile(_authService.GetUsernamesOfAllUsersLoggedIn().First());
            }
            _authService.Logout(username);
        }

        public void Send(IMailModel mail) {
            IncomingMailModel mailToSend = GetIncomingMailModel(mail);
            _currentUser.Client.Send(mailToSend);
        }

        public void SwitchAccount(string username) {
            SmtpUser user = _authService.GetApplicationUserProfile(username);
            if (user != null) {
                _currentUser = user;
            }
        }

        private IncomingMailModel GetIncomingMailModel(IMailModel mail) {
            return new IncomingMailModel(mail.From, mail.To) {
                Subject = mail.Subject,
                Body = mail.Body
            };
        }
    }
}
