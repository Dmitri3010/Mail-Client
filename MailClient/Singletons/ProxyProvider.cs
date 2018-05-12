using AutoMapper;
using MailClient.DAL.EF;
using MailClient.DAL.Interfaces;
using MailClient.DAL.Repositories;
using MailClient.IMap.Interfaces;
using MailClient.IMap.Proxy;

namespace MailClient.Singletons
{
    public class ApplicationProvider
    {
        private static ApplicationProvider provider;
        private readonly IUnitOfWork unitOfWork;
        private readonly IClientService clientService;

        private ApplicationProvider()
        {
            IMapper mapper = AutoMapperProvider.GetIMapper();

            ApplicationContext context = new ApplicationContext();
            unitOfWork = new UnitOfWork(context);
            clientService = new ClientServiceProxy(mapper, unitOfWork);
        }

        public static IClientService GetProxy()
        {
            if (provider == null)
            {
                provider = new ApplicationProvider();
            }
            return provider.clientService;
        }

        public static IUnitOfWork GetUnitOfWork() 
        {
            if (provider == null)
            {
                provider = new ApplicationProvider();
            }
            return provider.unitOfWork;
        }
    }
}
