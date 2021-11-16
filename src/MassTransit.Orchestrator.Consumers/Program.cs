using GreenPipes;

using MassTransit;
using MassTransit.Orchestrator.Consumers;

var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host("localhost", "/", h =>
    {
        h.Username("guest");
        h.Password("guest");
    });

    cfg.ReceiveEndpoint("account.create", ep =>
    {
        ep.PrefetchCount = 10;
        ep.UseMessageRetry(r => r.Interval(2, 100));
        ep.Consumer<EndpointConsumer>();
    });
});

var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
await busControl.StartAsync(source.Token);

try
{
    Console.WriteLine("Press enter to exit");

    await Task.Run(() => Console.ReadLine());
}
finally
{
    await busControl.StopAsync();
}