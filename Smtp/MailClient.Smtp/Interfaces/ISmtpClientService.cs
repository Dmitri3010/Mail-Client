using MailClient.Smtp.Models;
using MailClinet.Implementation.Models;

namespace MailClient.Smtp.Interfaces {
    public interface ISmtpClientService : ISmtpAuthService {
        SmtpUser User { get; }

        void SwitchAccount(string username);

        void Send(IMailModel mail);
    }
}
