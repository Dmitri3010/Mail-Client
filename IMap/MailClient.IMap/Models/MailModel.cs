using MailClinet.Implementation.Models;
using System;

namespace MailClient.Imap.Models
{
    public class MailModel : IMailModel
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }
    }
}
