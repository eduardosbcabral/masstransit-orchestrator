using MassTransit.Orchestrator.Shared;

using Newtonsoft.Json;

using System.Text;

namespace MassTransit.Orchestrator.Consumers
{
    public class EndpointConsumer : IConsumer<CreateAccountMessage>
    {
        public async Task Consume(ConsumeContext<CreateAccountMessage> context)
        {
            Console.WriteLine("account.create message consumed: {0}", context.Message.Name);
            
            HttpClient client = new();
            var json = JsonConvert.SerializeObject(context.Message);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync("http://localhost:5081/accounts/from-queue", httpContent);
        }
    }
}
