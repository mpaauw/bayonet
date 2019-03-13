using AndyC.Patterns.Commands;
using bayonet.Api.Commands.Items;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Data;
using Bogus;
using FakeItEasy;
using System;
using System.Threading.Tasks;

namespace bayonet.Api.Tests.Items
{
    public class GetItemCommandFixture
    {
        private readonly IWebService webService;
        private readonly ICommandRouter commandRouter;
        private readonly Faker faker;

        private string id;

        public GetItemCommandFixture()
        {
            this.webService = A.Fake<IWebService>();
            this.commandRouter = A.Fake<ICommandRouter>();
            this.faker = new Faker();
        }

        public GetItemCommandFixture WithValidId()
        {
            this.id = this.faker.Lorem.Word();
            return this;
        }

        public GetItemCommandFixture WithInvalidId()
        {
            string[] badData = { "", " ", null };
            this.id = badData[this.faker.Random.Int(0, 2)];
            return this;
        }

        public GetItemCommandFixture WithValidWebServiceGetContentAsyncResponse()
        {
            var item = Generators.FakeItem().Generate();
            A.CallTo(() => this.webService.GetContentAsync<Item>(A<string>._))
                .Returns(item);
            return this;
        }

        public GetItemCommandFixture WithWebServiceGetContentAsyncExceptionEncountered()
        {
            A.CallTo(() => this.webService.GetContentAsync<Item>(A<string>._))
                .Throws(new Exception());
            return this;
        }

        public async Task<Result<Item>> ExecuteCommandUnderTest()
        {
            var command = GetCommandUnderTest();
            var handler = new GetItemCommand.Handler(this.webService);
            return await handler.ExecuteAsync(command);
        }

        private GetItemCommand GetCommandUnderTest()
        {
            return new GetItemCommand(this.id);
        }
    }
}
