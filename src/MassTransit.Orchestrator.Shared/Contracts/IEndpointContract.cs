namespace MassTransit.Orchestrator.Shared.Contracts
{
    public interface IEndpointContract
    {
        public string QueueName { get; }
        public string Url { get; }
    }
}
