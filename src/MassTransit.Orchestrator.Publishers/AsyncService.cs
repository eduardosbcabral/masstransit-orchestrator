using MassTransit.Orchestrator.Shared.Contracts;

namespace MassTransit.Orchestrator.Publishers
{
    public class AsyncService : IAsyncService
    {
        private readonly ISendEndpointProvider _provider;

        public AsyncService(ISendEndpointProvider provider)
        {
            _provider = provider;
        }

        public async Task SendAsync<T>(T contract) where T : class, IEndpointContract
        {
            var uri = new Uri($"queue:{contract.QueueName}");
            var endpoint = await _provider.GetSendEndpoint(uri);
            await endpoint.Send(contract);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[PUBLISH] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Message sent to the queue: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(contract.QueueName);
            Console.ResetColor();
        }
    }

    public interface IAsyncService 
    {
        Task SendAsync<T>(T contract) where T : class, IEndpointContract;
    }
}
