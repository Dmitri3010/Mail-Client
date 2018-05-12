using S22.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.IMap.Models
{
    public class ApplicationUser
    {
        public string Hostname { get; set; }

        public string Username { get; set; }

        public ImapClient ImapClient { get; set; }
    }
}
