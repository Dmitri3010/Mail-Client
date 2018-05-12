using MailClient.IMap.Models;

namespace MailClient.IMap.Interfaces {
    public interface IClientService : IAuthService, IMailService {
        void SwitchAccount(string username);
    }
}
