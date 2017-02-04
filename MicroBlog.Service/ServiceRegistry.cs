using MicroBlog.Persistence;
using MicroBlog.Repository;
using StructureMap;

namespace MicroBlog.Service
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            For<IUnitOfWork>().Use<UnitOfWork>();
            For<IAuthRepository>().Use<AuthRepository>();
            For<IMicroBloggingService>().Use<MicroBloggingService>();
            For<IAccountService>().Use<AccountService>();
        }
    }
}
