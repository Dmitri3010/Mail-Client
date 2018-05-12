using AutoMapper;
using MailClient.DAL.EF;
using MailClient.Proxy;
using MailClient.Proxy.Interfaces;

namespace MailClient.Singletons
{
    public class ProxyProvider
    {
        private static ProxyProvider provider;
        private IUnitOfWorkProxy proxy;

        private ProxyProvider()
        {
            ApplicationContext context = new ApplicationContext();
            IMapper mapper = AutoMapperProvider.GetIMapper();
            proxy = new UnitOfWorkProxy(mapper, context);
        }

        public static IUnitOfWorkProxy GetProxy()
        {
            if (provider == null)
            {
                provider = new ProxyProvider();
            }
            return provider.proxy;
        }
    }
}
