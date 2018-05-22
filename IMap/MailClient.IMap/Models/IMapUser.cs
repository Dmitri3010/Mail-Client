using MailClinet.Implementation.Models;
using S22.Imap;

namespace MailClient.Imap.Models
{
    public class ImapUser : IApplicationUser<ImapClient>
    {
        public string Hostname { get; set; }

        public string Username { get; set; }

        public ImapClient Client { get; set; }
    }
}
