using MailClient.IMap.Models;
using S22.Imap;
using System.Collections.Generic;

namespace MailClient.IMap.Interfaces {
    public interface IMailService {
        ApplicationUser User { get; }

        List<MailModel> GetMessages(SearchCondition searchCondition);

        List<MailModel> GetNewMessages();

        List<MailModel> GetDeletedMessages();

        List<MailModel> GetSpamMessages();

        List<MailModel> GetRecentMessages();

        List<MailModel> GetUnseenMessages();

        List<MailModel> GetSentMessages();

        List<MailModel> GetAllMessages();
    }
}
