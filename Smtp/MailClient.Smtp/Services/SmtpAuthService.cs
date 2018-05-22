using MailClient.Smtp.Interfaces;
using MailClient.Smtp.Models;
using MailClinet.Implementation.Services;
using System.Net;
using System.Net.Mail;

namespace MailClient.Smtp.Services {
    public class SmtpAuthService : AuthService<SmtpUser, SmtpClient>, ISmtpAuthService {
        public SmtpAuthService() : base() {
        }

        protected override SmtpUser GetApplicationUserModel(string hostname, string username, string password) {
            return new SmtpUser {
                Hostname = hostname,
                Username = username,
                Client = GetSmtpClient(hostname, username, password)
            };
        }

        private SmtpClient GetSmtpClient(string hostname, string username, string password) {
            return new SmtpClient(hostname, 587) {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };
        }
    }
}
