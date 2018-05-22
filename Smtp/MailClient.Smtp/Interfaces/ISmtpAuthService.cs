using MailClient.Smtp.Models;
using MailClinet.Implementation.Interfaces;
using System.Net.Mail;

namespace MailClient.Smtp.Interfaces 
{
    public interface ISmtpAuthService : IAuthService<SmtpUser, SmtpClient>
    {
    }
}
