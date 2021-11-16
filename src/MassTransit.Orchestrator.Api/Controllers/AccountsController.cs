using MassTransit.Orchestrator.Publishers;
using MassTransit.Orchestrator.Shared;

using Microsoft.AspNetCore.Mvc;

namespace MassTransit.Orchestrator.Api.Controllers
{
    [Route("accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly ICustomPublisherService _customPublisher;

        public AccountsController(ICustomPublisherService customPublisher)
        {
            _customPublisher = customPublisher;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountMessage message)
        {
            if(message == null)
            {
                return BadRequest();
            }

            await _customPublisher.Accounts.CreateAsync(message);
            return Ok();
        }

        [HttpPost("from-queue")]
        public IActionResult CreateFromQueue(CreateAccountMessage message)
        {
            if (message == null)
            {
                return BadRequest();
            }

            Console.WriteLine("[ENDPOINT][from-queue] Message sent from the consumer to the endpoint and the account was created from queue");
            return Ok();
        }
    }
}
