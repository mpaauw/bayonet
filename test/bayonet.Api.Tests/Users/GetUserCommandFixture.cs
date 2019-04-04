using bayonet.Api.Commands.Users;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Data;
using bayonet.Tests.Common;
using Bogus;
using FakeItEasy;
using System;
using System.Threading.Tasks;

namespace bayonet.Api.Tests.Users
{
    public class GetUserCommandFixture
    {
        private readonly IWebService webService;
        private readonly Faker faker;

        private string id;

        public GetUserCommandFixture()
        {
            this.webService = A.Fake<IWebService>();
            this.faker = new Faker();
        }

        public GetUserCommandFixture WithInvalidId()
        {
            string[] badData = new string[] { "", " ", null };
            this.id = badData[this.faker.Random.Int(0, 2)];
            return this;
        }

        public GetUserCommandFixture WithValidId()
        {
            this.id = this.faker.Lorem.Word();
            return this;
        }

        public GetUserCommandFixture WithWebServiceGetContentAsyncUserExceptionEncountered()
        {
            A.CallTo(() => this.webService.GetContentAsync<User>(A<string>._))
                .Throws(new Exception());
            return this;
        }

        public GetUserCommandFixture WithValidWebServiceGetContentAsyncUserResponse()
        {
            var fakeUser = Generators.FakeUser().Generate();
            A.CallTo(() => this.webService.GetContentAsync<User>(A<string>._))
                .Returns(fakeUser);
            return this;
        }

        public async Task<Result<User>> ExecuteCommandUnderTest()
        {
            var command = GetCommandUnderTest();
            var handler = new GetUserCommand.Handler(this.webService);
            return await handler.ExecuteAsync(command);
        }

        private GetUserCommand GetCommandUnderTest()
        {
            return new GetUserCommand(this.id);
        }
    }
}
