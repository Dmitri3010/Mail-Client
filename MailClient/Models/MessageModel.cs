using System;

namespace MailClient.Models
{
    public class MessageModel
    {
        public string From { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }
    }
}
