using System;
using EricBach.LambdaLogger;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pecuniary.Account.Data.Commands;
using Pecuniary.Account.Data.Responses;
using Pecuniary.Account.Data.ViewModels;

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
        public ActionResult<CommandResponse> Post([FromBody] AccountViewModel vm)
        {
            Logger.Log($"Received {nameof(CreateAccountCommand)}");

            var id = Guid.NewGuid();
            
            try
            {
                _mediator.Send(new CreateAccountCommand(id, vm));
            }
            catch (Exception e)
            {
                return BadRequest(new CommandResponse { Id = id, Error= e.Message});
            }

            Logger.Log($"Completed processing {nameof(CreateAccountCommand)}");

            return Ok(new CommandResponse {Id = id, Name = nameof(CreateAccountCommand)});
        }

        // PUT api/acccount/5
        [HttpPut("{id}")]
        public ActionResult<CommandResponse> Put(Guid id, [FromBody] AccountViewModel vm)
        {
            Logger.Log($"Received {nameof(UpdateAccountCommand)}");

            try
            {
                _mediator.Send(new UpdateAccountCommand(id, vm));
            }
            catch (Exception e)
            {
                return BadRequest(new CommandResponse { Id = id, Error = e.Message });
            }

            Logger.Log($"Completed processing {nameof(UpdateAccountCommand)}");

            return Ok(new CommandResponse { Id = id, Name = nameof(UpdateAccountCommand) });
        }
    }
}
