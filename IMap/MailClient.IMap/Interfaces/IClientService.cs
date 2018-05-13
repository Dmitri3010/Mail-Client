namespace MailClient.Imap.Interfaces {
    public interface IClientService : IImapAuthService, IMailService {
        void SwitchAccount(string username);
    }
}
