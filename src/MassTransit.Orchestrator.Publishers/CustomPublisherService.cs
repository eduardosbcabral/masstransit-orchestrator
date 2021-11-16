using Microsoft.Extensions.Logging;

namespace MassTransit.Orchestrator.Publishers
{
    public class CustomPublisherService : ICustomPublisherService
    {
        public IAccountsResource Accounts { get; private set; }

        public CustomPublisherService(IBus bus, ILogger<CustomPublisherService> logger)
        {
            Accounts = new AccountsResource(bus, logger);
        }
    }

    public interface ICustomPublisherService 
    {
        IAccountsResource Accounts { get; }
    }
}
