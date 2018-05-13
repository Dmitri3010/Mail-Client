using MailClient.Imap.Models;
using S22.Imap;
using MailClient.Imap.Interfaces;
using MailClinet.Implementation.Services;

namespace MailClient.Imap.Services
{
    public class ImapAuthService : AuthService<ImapUser, ImapClient>, IImapAuthService {
        public ImapAuthService() : base() {
        }

        protected override ImapUser GetApplicationUserModel(string hostname, string username, string password)
        {
            return new ImapUser
            {
                Hostname = hostname,
                Username = username,
                Client = GetImapClient(hostname, username, password)
            };
        }

        private ImapClient GetImapClient(string hostname, string username, string password)
        {
            return new ImapClient(hostname, 993, username, password, AuthMethod.Login, true);
        }
    }
}
