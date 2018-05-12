using MailClient.DAL.EF;
using MailClient.DAL.Interfaces;

namespace MailClient.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            _userRepository = new UserRepository(_context);
            _messageRepository = new MessageRepository(_context);
        }

        public ApplicationContext Context => _context;

        public IUserRepository UserRepository => _userRepository;

        public IMessageRepository MessageRepository => _messageRepository;

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
