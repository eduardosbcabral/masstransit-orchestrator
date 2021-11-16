using MassTransit.Orchestrator.Shared.Contracts;

using Newtonsoft.Json;

using System.Text;

namespace MassTransit.Orchestrator.Consumers
{
    public class EndpointConsumer : IConsumer<CreateAccount>
    {
        public async Task Consume(ConsumeContext<CreateAccount> context)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[CONSUMER] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Message consumed from queue: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(context.Message.QueueName);
            Console.ResetColor();

            HttpClient client = new();
            var json = JsonConvert.SerializeObject(context.Message);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync(context.Message.Url, httpContent);
        }
    }
}
