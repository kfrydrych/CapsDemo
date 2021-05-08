using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CapsDemo.ServiceOne.Employees;

namespace CapsDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Employees : ControllerBase
    {
        private readonly IMediator _mediator;

        public Employees(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var command = new CreateEmployee
            {
                Name = $"Diminic-{DateTime.Now.ToLongDateString()}"
            };

            await _mediator.Send(command);

            return Ok();

        }
    }
}
