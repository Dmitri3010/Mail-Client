using MailClient.Imap.Models;
using MailClinet.Implementation.Interfaces;
using S22.Imap;

namespace MailClient.Imap.Interfaces {
    public interface IImapAuthService : IAuthService<ImapUser, ImapClient> 
    {
    }
}
