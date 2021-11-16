namespace MassTransit.Orchestrator.Shared.Contracts
{
    public record CreateAccount : IEndpointContract
    {
        public string QueueName => "account.create";
        public string Url => "http://localhost:5081/accounts/from-queue"; 

        public string Name { get; set; }
    }
}
