using System;
using System.Threading;
using System.Threading.Tasks;
using EricBach.CQRS.EventRepository;
using EricBach.LambdaLogger;
using MediatR;
using Pecuniary.Account.Data.Commands;
using _Account = Pecuniary.Account.Data.Models.Account;

namespace Pecuniary.Account.Command.CommandHandlers
{
    public class AccountCommandHandlers : IRequestHandler<CreateAccountCommand, CancellationToken>,
        IRequestHandler<UpdateAccountCommand, CancellationToken>
    {
        private readonly IEventRepository<_Account> _repository;

        public AccountCommandHandlers(IEventRepository<_Account> repository)
        {
            _repository = repository ?? throw new InvalidOperationException("Repository is not initialized.");
        }

        public Task<CancellationToken> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
        {
            Logger.Log($"{nameof(CreateAccountCommand)} handler invoked");

            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (string.IsNullOrEmpty(command.Account.Name))
                throw new Exception($"{command.Account.Name} is required");

            // TODO Figure out how to validate this from the "database" without making it a long synchronous call
            if (string.IsNullOrEmpty(command.Account.AccountTypeCode) || command.Account.AccountTypeCode != "LIRA" && command.Account.AccountTypeCode != "TFSA" &&
                command.Account.AccountTypeCode != "RESP" && command.Account.AccountTypeCode != "RRSP" && command.Account.AccountTypeCode != "Unreg")
                throw new Exception("Invalid command.Account.AccountTypeCode. Must be one of values [LIRA, RESP, RRSP, TFSA, UnReg]");

            var aggregate = new _Account(command.Id, command.Account);

            // Save to Event Store
            _repository.Save(aggregate, aggregate.Version);

            Logger.Log($"Completed saving {nameof(Account)} aggregate to event store");

            return Task.FromResult(cancellationToken);
        }

        public Task<CancellationToken> Handle(UpdateAccountCommand command, CancellationToken cancellationToken)
        {
            Logger.Log($"{nameof(UpdateAccountCommand)} handler invoked");

            if (command == null)
                throw new ArgumentNullException(nameof(command));

            if (string.IsNullOrEmpty(command.Account.Name))
                throw new Exception($"{command.Account.Name} is required");

            Logger.Log($"Looking for aggregate: {command.Id}");

            var aggregate = _repository.GetById(command.Id);

            Logger.Log($"Found existing aggregate to update: {aggregate.Id}");

            // Validate that AccountId exists
            if (aggregate.Id == Guid.Empty)
                throw new Exception($"Account with Id {aggregate.Id} does not exist");

            aggregate.UpdateAccount(command.Account);

            Logger.Log($"Updated aggregate {aggregate.Id}");

            _repository.Save(aggregate, aggregate.Version);

            Logger.Log($"Completed saving {nameof(Account)} aggregate to event store");

            return Task.FromResult(cancellationToken);

        }
    }
}
