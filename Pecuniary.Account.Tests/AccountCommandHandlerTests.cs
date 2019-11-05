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
using Pecuniary.Account.Command;
using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pecuniary.Account.Tests
{
    public class AccountCommandHandlerTests
    {
        [Fact]
        public void Test()
        {

        }

        [Fact]
        public void ConfigureServices_RegistersDependenciesCorrectly()
        {
            //  Arrange

            //  Setting up the stuff required for Configuration.GetConnectionString("DefaultConnection")
            Mock<IConfigurationSection> configurationSectionStub = new Mock<IConfigurationSection>();
            Mock<Microsoft.Extensions.Configuration.IConfiguration> configurationStub = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            configurationStub.Setup(x => x.GetSection("ConnectionStrings")).Returns(configurationSectionStub.Object);

            IServiceCollection services = new ServiceCollection();
            var target = new Pecuniary.Account.Command.Startup(configurationStub.Object);

            //  Act

            target.ConfigureServices(services);
            //  Mimic internal asp.net core logic.
            services.AddTransient<AccountController>();

            //  Assert

            var serviceProvider = services.BuildServiceProvider();

            var controller = serviceProvider.GetService<AccountController>();
            controller.Should().NotBeNull();
        }
    }
}
