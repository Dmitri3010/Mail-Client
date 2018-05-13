using MailClient.Imap.Models;
using S22.Imap;
using System.Collections.Generic;

namespace MailClient.Imap.Interfaces {
    public interface IMailService {
        ImapUser User { get; }

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
