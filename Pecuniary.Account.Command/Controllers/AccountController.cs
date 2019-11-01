using System;
using EricBach.CQRS.Commands;
using EricBach.LambdaLogger;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pecuniary.Account.Data.Commands;
using Pecuniary.Account.Data.Requests;

namespace Pecuniary.Account.Command.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // POST api/account
        [HttpPost]
        public ActionResult<CommandResponse> Post([FromBody] CreateAccountRequest request)
        {
            Logger.Log($"Received {nameof(CreateAccountRequest)}");

            var id = Guid.NewGuid();
            
            try
            {
                _mediator.Send(new CreateAccountCommand(id, request));
            }
            catch (Exception e)
            {
                return BadRequest(new CommandResponse { Error= e.Message });
            }

            Logger.Log($"Completed processing {nameof(CreateAccountRequest)}");

            return Ok(new CommandResponse {Id = id, Name = nameof(CreateAccountCommand) });
        }

        // PUT api/acccount/5
        [HttpPut("{id}")]
        public ActionResult<CommandResponse> Put(Guid id, [FromBody] UpdateAccountRequest request)
        {
            Logger.Log($"Received {nameof(UpdateAccountRequest)}");

            try
            {
                _mediator.Send(new UpdateAccountCommand(id, request));
            }
            catch (Exception e)
            {
                return BadRequest(new CommandResponse { Id = id, Error = e.Message });
            }

            Logger.Log($"Completed processing {nameof(UpdateAccountRequest)}");

            return Ok(new CommandResponse { Id = id, Name = nameof(UpdateAccountCommand) });
        }
    }
}
