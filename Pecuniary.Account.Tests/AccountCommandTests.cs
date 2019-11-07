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
        [Fact]
        public void ShouldCreateAccountOnSuccess()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<CreateAccountCommand>(), It.IsAny<CancellationToken>()));

            var controller = new AccountController(mediatorMock.Object);

            var request = new CreateAccountRequest
            {
                Name = "Test",
                AccountTypeCode = "Test"
            };
            
            // Act
            var response = controller.Post(request);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            var result = response.Result as OkObjectResult;
            result.Should().NotBe(null);
            var value = result.Value as CommandResponse;

            result.StatusCode.Should().Be(200);
            result.Value.Should().BeOfType<CommandResponse>();
            value.Name.Should().Be(nameof(CreateAccountCommand));
            value.Id.Should().NotBe(Guid.Empty);
            value.Error.Should().BeNullOrEmpty();
        }

        [Fact]
        public void ShouldNotCreateAccountOnException()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<CreateAccountCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            var controller = new AccountController(mediatorMock.Object);

            var request = new CreateAccountRequest
            {
                Name = "Test",
                AccountTypeCode = "Test"
            };

            // Act
            var response = controller.Post(request);

            // Assert
            response.Result.Should().BeOfType<BadRequestObjectResult>();
            var result = response.Result as BadRequestObjectResult;
            result.Should().NotBe(null);
            var value = result.Value as CommandResponse;

            result.StatusCode.Should().Be(400);
            value.Should().BeOfType<CommandResponse>();
            value.Id.Should().Be(Guid.Empty);
            value.Error.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ShouldUpdateAccountOnSuccess()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateAccountCommand>(), It.IsAny<CancellationToken>()));

            var controller = new AccountController(mediatorMock.Object);

            var id = Guid.NewGuid();
            var request = new UpdateAccountRequest
            {
                Name = "Test",
                AccountTypeCode = "Test"
            };

            // Act
            var response = controller.Put(id, request);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            var result = response.Result as OkObjectResult;
            result.Should().NotBe(null);
            var value = result.Value as CommandResponse;

            result.StatusCode.Should().Be(200);
            result.Value.Should().BeOfType<CommandResponse>();
            value.Name.Should().Be(nameof(UpdateAccountCommand));
            value.Id.Should().Be(id);
            value.Error.Should().BeNullOrEmpty();
        }

        [Fact]
        public void ShouldNotUpdateAccountOnException()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateAccountCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());

            var controller = new AccountController(mediatorMock.Object);

            var id = Guid.NewGuid();
            var request = new UpdateAccountRequest
            {
                Name = "Test",
                AccountTypeCode = "Test"
            };

            // Act
            var response = controller.Put(id, request);

            // Assert
            response.Result.Should().BeOfType<BadRequestObjectResult>();
            var result = response.Result as BadRequestObjectResult;
            result.Should().NotBe(null);
            var value = result.Value as CommandResponse;

            result.StatusCode.Should().Be(400);
            value.Should().BeOfType<CommandResponse>();
            value.Id.Should().Be(id);
            value.Error.Should().NotBeNullOrEmpty();
        }
    }
}
