using bayonet.Api.Commands.Users;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Data;
using Bogus;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Api.Tests.Users
{
    public class GetUpdatedUsersCommandFixture
    {
        private readonly IWebService webService;
        private readonly Faker faker;

        private int count;

        public GetUpdatedUsersCommandFixture()
        {
            this.webService = A.Fake<IWebService>();
            this.faker = new Faker();
        }

        public GetUpdatedUsersCommandFixture WithInvalidCount()
        {
            int[] badData = { -1, 0 };
            this.count = badData[this.faker.Random.Int(0, 1)];
            return this;
        }

        public GetUpdatedUsersCommandFixture WithValidCount()
        {
            this.count = this.faker.Random.Int(1, 10);
            return this;
        }

        public GetUpdatedUsersCommandFixture WithWebServiceGetContentAsyncUpdatesExceptionEncountered()
        {
            A.CallTo(() => this.webService.GetContentAsync<Updates>(A<string>._))
                .Throws(new Exception());
            return this;
        }

        public GetUpdatedUsersCommandFixture WithValidWebServiceGetContentAsyncUpdatesResponse()
        {
            var fakeUpdates = Generators.FakeUpdates().Generate();
            A.CallTo(() => this.webService.GetContentAsync<Updates>(A<string>._))
                .Returns(fakeUpdates);
            return this;
        }

        public GetUpdatedUsersCommandFixture WithValidWebServiceGetContentAsyncUserResponse()
        {
            var fakeUser = Generators.FakeUser().Generate();
            A.CallTo(() => this.webService.GetContentAsync<User>(A<string>._))
                .Returns(fakeUser);
            return this;
        }

        public async Task<Result<IEnumerable<User>>> ExecuteCommandUnderTest()
        {
            var command = GetCommandUnderTest();
            var handler = new GetUpdatedUsersCommand.Handler(this.webService);
            return await handler.ExecuteAsync(command);
        }

        private GetUpdatedUsersCommand GetCommandUnderTest()
        {
            return new GetUpdatedUsersCommand(this.count);
        }
    }
}
