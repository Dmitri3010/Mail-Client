using MailClient.DAL.Entities;
using System;

namespace MailClient.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Guid GetIdByUsername(string username);
    }
}
