using MailClient.DAL.EF;
using MailClient.DAL.Entities;
using MailClient.DAL.Interfaces;
using System;
using System.Linq;

namespace MailClient.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }

        public Guid GetIdByUsername(string username)
        {
            User user = _context.Users.FirstOrDefault(u => u.Username.Equals(username));
            if (user == null)
            {
                return Guid.Empty;
            }
            return user.Id;
        }
    }
}
