using MassTransit;
using MassTransit.Orchestrator.Publishers;

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MassTransit.Orchestrator.Api", Version = "v1" });
});
builder.Services.AddLogging();

builder.Services.AddMassTransit(x =>
{
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    }));
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddScoped<ICustomPublisherService>(x => 
{
    var bus = x.GetService<IBus>();
    var logger = x.GetService<ILogger<CustomPublisherService>>();
    return new CustomPublisherService(bus, logger);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MassTransit.Orchestrator.Api v1");
    });
}

app.UseRouting();

app.UseEndpoints(x =>
{
    x.MapControllers();
});

app.Run();
