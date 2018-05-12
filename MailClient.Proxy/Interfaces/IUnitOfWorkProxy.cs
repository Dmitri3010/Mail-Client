using AutoMapper;
using MailClient.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Proxy.Interfaces
{
    public interface IUnitOfWorkProxy : IUnitOfWork
    {
        IMapper Mapper { get; }

        IMessageRepositoryProxy MessageRepositoryProxy { get; }
    }
}
