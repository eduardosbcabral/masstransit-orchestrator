using MassTransit.Orchestrator.Api.Models;
using MassTransit.Orchestrator.Publishers;
using MassTransit.Orchestrator.Shared.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace MassTransit.Orchestrator.Api.Controllers
{
    [Route("accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAsyncService _customPublisher;

        public AccountsController(IAsyncService customPublisher)
        {
            _customPublisher = customPublisher;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountRequest request)
        {
            if(request == null)
            {
                return BadRequest();
            }

            await _customPublisher.SendAsync(new CreateAccount
            {
                Name = request.Name,
            });
            return Ok();
        }

        [HttpPost("from-queue")]
        public IActionResult CreateFromQueue(CreateAccountRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[ENDPOINT][from-queue] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Request sent and message processed");
            Console.ResetColor(); 
            
            return Ok();
        }
    }
}
