using System;
using System.Threading;
using EricBach.CQRS.Commands;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pecuniary.Account.Command.Controllers;
using Pecuniary.Account.Data.Commands;
using Pecuniary.Account.Data.Requests;
using Xunit;

namespace Pecuniary.Account.Tests
{
    public class AccountCommandTests
    {
        [Theory]
        [InlineData("LIRA")]
        [InlineData("TSFA")]
        [InlineData("RESP")]
        [InlineData("RRSP")]
        [InlineData("UnReg")]
        public void ShouldCreateAccount(string accountTypeCode)
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<CreateAccountCommand>(), It.IsAny<CancellationToken>()));

            var controller = new AccountController(mediatorMock.Object);

            var request = new CreateAccountRequest
            {
                Name = "Test",
                AccountTypeCode = accountTypeCode
            };
            
            // Act
            var response = controller.Post(request);

            // Assert
            var result = response.Result as OkObjectResult;
            result.Should().NotBe(null);
            var value = result.Value as CommandResponse;

            response.Result.Should().BeOfType<OkObjectResult>();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeOfType<CommandResponse>();
            value.Name.Should().Be(nameof(CreateAccountCommand));
            value.Id.Should().NotBe(Guid.Empty);
            value.Error.Should().BeNullOrEmpty();
        }
    }
}
