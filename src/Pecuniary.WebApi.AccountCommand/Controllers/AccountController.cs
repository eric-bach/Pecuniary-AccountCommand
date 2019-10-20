using System;
using Logging.LambdaLogger;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pecuniary.Commands.Account;
using Pecuniary.ViewModels;
using Pecuniary.ViewModels.Responses;

namespace Pecuniary.WebApi.AccountCommand.Controllers
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

            vm.Id = Guid.NewGuid();
            
            try
            {
                _mediator.Send(new CreateAccountCommand(vm));
            }
            catch (Exception e)
            {
                return BadRequest(new CommandResponse { Id = vm.Id, Error= e.Message});
            }

            Logger.Log($"Completed processing {nameof(CreateAccountCommand)}");

            return Ok(new CommandResponse {Id = vm.Id, Name = nameof(CreateAccountCommand)});
        }

        // PUT api/acccount/5
        [HttpPut("{id}")]
        public ActionResult<CommandResponse> Put(Guid id, [FromBody] AccountViewModel vm)
        {
            Logger.Log($"Received {nameof(UpdateAccountCommand)}");

            vm.Id = id;
            try
            {
                _mediator.Send(new UpdateAccountCommand(vm));
            }
            catch (Exception e)
            {
                return BadRequest(new CommandResponse { Id = vm.Id, Error = e.Message });
            }

            Logger.Log($"Completed processing {nameof(UpdateAccountCommand)}");

            return Ok(new CommandResponse { Id = vm.Id, Name = nameof(UpdateAccountCommand) });
        }
    }
}
