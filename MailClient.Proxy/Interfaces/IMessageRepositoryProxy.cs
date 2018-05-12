using MailClient.DAL.Entities;
using MailClient.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Proxy.Interfaces
{
    public interface IMessageRepositoryProxy : IMessageRepository
    {
        bool ForceUpdate { get; set; }

        bool IsAnyUserLoggedIn();

        void Login(string hostname, string username, string password);

        void Logout(string username);

        void SwitchAccount(string username);

        IEnumerable<Message> GetAllMessages();

        IEnumerable<Message> GetNewMessages();

        IEnumerable<Message> GetSentMessages();

        IEnumerable<Message> GetDeletedMessages();

        IEnumerable<Message> GetRecentMessages();

        IEnumerable<Message> GetUnseenMessages();


        IEnumerable<Message> GetSpamMessages();
    }
}
