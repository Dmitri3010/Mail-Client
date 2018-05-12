using MailClient.DAL.Entities;
using System.Collections.Generic;

namespace MailClient.DAL.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        IEnumerable<Message> GetAllUserMessages(string username);

        IEnumerable<Message> AddRangeWhichNotIncluded(IEnumerable<Message> messages);

        IEnumerable<Message> AddRangeWhichNotIncludedAndSetOwnerUserId(IEnumerable<Message> messages, string username);
    }
}
