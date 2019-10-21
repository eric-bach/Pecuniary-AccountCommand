using System;
using System.Threading;
using System.Threading.Tasks;
using EricBach.CQRS.Repository;
using EricBach.LambdaLogger;
using MediatR;
using Pecuniary.Account.Data.Commands;
using Accounts = Pecuniary.Account.Data.Models.Account;

namespace Pecuniary.Account.Command.CommandHandlers
{
    public class AccountCommandHandlers : IRequestHandler<CreateAccountCommand, CancellationToken>,
        IRequestHandler<UpdateAccountCommand, CancellationToken>
    {
        private readonly IEventRepository<Accounts> _repository;

        public AccountCommandHandlers(IEventRepository<Account.Data.Models.Account> repository)
        {
            _repository = repository ?? throw new InvalidOperationException("Repository is not initialized.");
        }

        public Task<CancellationToken> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
        {
            Logger.Log($"{nameof(CreateAccountCommand)} handler invoked");

            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var aggregate = new Accounts(command.Id, command.Account);

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

            Logger.Log($"Looking for aggregate: {command.Id}");

            var aggregate = _repository.GetById(command.Id);

            Logger.Log($"Found existing aggregate to update: {aggregate.Id}");

            aggregate.UpdateAccount(command.Account, aggregate.Version);

            Logger.Log($"Updated aggregate {aggregate.Id}");

            _repository.Save(aggregate, aggregate.Version);

            Logger.Log($"Completed saving {nameof(Account)} aggregate to event store");

            return Task.FromResult(cancellationToken);

        }
    }
}
