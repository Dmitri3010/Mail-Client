using AutoMapper;
using MailClient.DAL.EF;
using MailClient.DAL.Interfaces;
using MailClient.DAL.Repositories;
using MailClient.Proxy.Interfaces;
using MailClient.Proxy.Repositories;
using System;

namespace MailClient.Proxy
{
    public class UnitOfWorkProxy : IUnitOfWorkProxy
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        private readonly IMessageRepositoryProxy _messageRepositoryProxy;

        public UnitOfWorkProxy(IMapper mapper, ApplicationContext context)
        {
            _database = new UnitOfWork(context);
            _mapper = mapper;
            _messageRepositoryProxy = new MessageRepositoryProxy(_mapper, _database.MessageRepository);
        }

        public ApplicationContext Context => throw new NotImplementedException();

        public IMapper Mapper => _mapper;

        public IUserRepository UserRepository => _database.UserRepository;

        public IMessageRepository MessageRepository => _database.MessageRepository;

        public IMessageRepositoryProxy MessageRepositoryProxy => _messageRepositoryProxy;

        public void SaveChanges()
        {
            _database.SaveChanges();
        }
    }
}
