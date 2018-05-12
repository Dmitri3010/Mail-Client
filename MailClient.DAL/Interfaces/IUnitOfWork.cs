using MailClient.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ApplicationContext Context { get; }

        IUserRepository UserRepository { get; }

        IMessageRepository MessageRepository { get; }

        void SaveChanges();
    }
}
