using MailClient.DAL.EF;
using MailClient.DAL.Entities;
using MailClient.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailClient.DAL.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationContext context) : base(context)
        {
        }

        public override IEnumerable<Message> GetAll()
        {
            return _context.Messages.OrderByDescending(m => m.Date);
        }

        public IEnumerable<Message> GetAllUserMessages(string username)
        {
            User user = _context.Users.FirstOrDefault(u => u.Username.Equals(username));
            if (user == null)
            {
                return null;
            }
            return _context.Messages.Where(m => m.UserId == user.Id).OrderByDescending(m => m.Date);
        }

        public IEnumerable<Message> AddRangeWhichNotIncluded(IEnumerable<Message> messages)
        {
            messages = QueryThroughDatabaseToGetMessagesWhichNotInIt(messages);
            return AddRange(messages);
        }

        public IEnumerable<Message> AddRangeWhichNotIncludedAndSetOwnerUserId(IEnumerable<Message> messages, string username)
        {
            User user = _context.Users.FirstOrDefault(u => u.Username.Equals(username));
            if (user == null)
            {
                return null;
            }

            messages = QueryThroughDatabaseToGetMessagesWhichNotInIt(messages);
            messages = QueryThrougMessageCollectionToSetUserId(messages, user.Id);
            return AddRange(messages);
        }

        private IEnumerable<Message> QueryThroughDatabaseToGetMessagesWhichNotInIt(IEnumerable<Message> messages)
        {
            return messages.Where(m => !_context.Messages.Select(dbm => dbm.Subject).Contains(m.Subject));
        }

        private IEnumerable<Message> QueryThrougMessageCollectionToSetUserId(IEnumerable<Message> messages, Guid userId)
        {
            return messages.Select(m => {
                m.UserId = userId;
                return m;
            });
        } 
    }
}
