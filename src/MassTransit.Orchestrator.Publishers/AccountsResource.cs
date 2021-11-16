using MassTransit.Orchestrator.Shared;

using Microsoft.Extensions.Logging;

namespace MassTransit.Orchestrator.Publishers
{
    public class AccountsResource : IAccountsResource
    {
        private readonly IBus _bus;
        private readonly ILogger<CustomPublisherService> _logger;

        public AccountsResource(IBus bus, ILogger<CustomPublisherService> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task CreateAsync(CreateAccountMessage message)
        {
            var uri = new Uri("rabbitmq://localhost/account.create");
            var endpoint = await _bus.GetSendEndpoint(uri);
            await endpoint.Send(message);
            _logger.LogInformation("[PUBLISH] Message sent to queue 'account.create'.");
        }
    }

    public interface IAccountsResource 
    {
        Task CreateAsync(CreateAccountMessage message);
    }
}
