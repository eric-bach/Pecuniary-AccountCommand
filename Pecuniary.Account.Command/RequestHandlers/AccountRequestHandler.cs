using System;
using System.Threading;
using System.Threading.Tasks;
using EricBach.CQRS.EventRepository;
using MediatR;
using Pecuniary.Account.Data.Commands;
using Pecuniary.Account.Data.Requests;

namespace Pecuniary.Account.Command.RequestHandlers
{
    public class AccountRequestHandler : IRequestHandler<CreateAccountRequest, CancellationToken>,
        IRequestHandler<UpdateAccountRequest, CancellationToken>
    {
        private readonly IMediator _mediator;
        private readonly IEventRepository<Data.Models.Account> _eventRepository;

        public AccountRequestHandler(IMediator mediator, IEventRepository<Data.Models.Account> eventRepository)
        {
            _mediator = mediator;
            _eventRepository = eventRepository;
        }

        public Task<CancellationToken> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
                throw new Exception("Name is required");

            // TODO Figure out how to validate this from the "database" without making it a long synchronous call
            if (string.IsNullOrEmpty(request.AccountTypeCode) || request.AccountTypeCode != "LIRA" && request.AccountTypeCode != "TFSA" &&
                request.AccountTypeCode != "RESP" && request.AccountTypeCode != "RRSP" && request.AccountTypeCode != "Unreg")
                throw new Exception("Invalid request.AccountTypeCode. Must be one of values [LIRA, RESP, RRSP, TFSA, UnReg]");

            // TODO Use AutoMapper
            var createAccount = new CreateAccount
            {
                Name = request.Name,
                AccountTypeCode = request.AccountTypeCode
            };
            
            _mediator.Send(new CreateAccountCommand(request.Id, createAccount), CancellationToken.None);

            return Task.FromResult(cancellationToken);
        }

        public Task<CancellationToken> Handle(UpdateAccountRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
                throw new Exception("Name is required");

            // TODO Use AutoMapper
            var updateAccount = new UpdateAccount
            {
                Name = request.Name
            };

            _mediator.Send(new UpdateAccountCommand(request.Id, updateAccount, _eventRepository), CancellationToken.None);

            return Task.FromResult(cancellationToken);
        }
    }
}
