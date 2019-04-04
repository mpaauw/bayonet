using bayonet.Api.Commands.Items;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Data;
using bayonet.Tests.Common;
using Bogus;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bayonet.Api.Tests.Items
{
    public class GetUpdatedItemsCommandFixture
    {
        private readonly IWebService webService;
        private readonly Faker faker;

        private int count;

        public GetUpdatedItemsCommandFixture()
        {
            this.webService = A.Fake<IWebService>();
            this.faker = new Faker();
        }

        public GetUpdatedItemsCommandFixture WithInvalidCount()
        {
            int[] badData = { -1, 0 };
            this.count = badData[this.faker.Random.Int(0, 1)];
            return this;
        }

        public GetUpdatedItemsCommandFixture WithValidCount()
        {
            this.count = this.faker.Random.Int(1, 10);
            return this;
        }

        public GetUpdatedItemsCommandFixture WithWebServiceGetContentAsyncUpdatesExceptionEncountered()
        {
            A.CallTo(() => this.webService.GetContentAsync<Updates>(A<string>._))
                .Throws(new Exception());
            return this;
        }

        public GetUpdatedItemsCommandFixture WithValidWebServiceGetContentAsyncUpdatesResponse()
        {
            var updates = Generators.FakeUpdates().Generate();
            A.CallTo(() => this.webService.GetContentAsync<Updates>(A<string>._))
                .Returns(updates);
            return this;
        }

        public GetUpdatedItemsCommandFixture WithValidWebServiceGetContentAsyncItemResponse()
        {
            var item = Generators.FakeItem().Generate();
            A.CallTo(() => this.webService.GetContentAsync<Item>(A<string>._))
                .Returns(item);
            return this;
        }

        public async Task<Result<IEnumerable<Item>>> ExecuteCommandUnderTest()
        {
            var command = GetCommandUnderTest();
            var handler = new GetUpdatedItemsCommand.Handler(this.webService);
            return await handler.ExecuteAsync(command);
        }

        private GetUpdatedItemsCommand GetCommandUnderTest()
        {
            return new GetUpdatedItemsCommand(this.count);
        }
    }
}
