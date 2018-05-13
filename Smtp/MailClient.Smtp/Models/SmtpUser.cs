using MailClinet.Implementation.Models;
using System.Net.Mail;

namespace MailClient.Smtp.Models 
{
    public class SmtpUser : IApplicationUser<SmtpClient>
    {
        public string Hostname { get; set; }

        public string Username { get; set; }

        public SmtpClient Client { get; set; }
    }
}
