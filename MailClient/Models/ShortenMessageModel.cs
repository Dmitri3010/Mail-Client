using System;

namespace MailClient.Models
{
    public class ShortenMessageModel
    {
        public string From { get; set; }

        public string Subject { get; set; }

        public DateTime Date { get; set; }
    }
}
