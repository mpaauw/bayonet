using AndyC.Patterns.Commands;
using bayonet.Api.Commands.Items;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Data;
using bayonet.Tests.Common;
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
        private bool retrieveChildren;

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

        public GetItemCommandFixture WithRetrieveChildren()
        {
            this.retrieveChildren = true;
            return this;
        }

        public GetItemCommandFixture WithValidWebServiceGetContentAsyncResponse()
        {
            A.CallTo(() => this.webService.GetContentAsync<Item>(A<string>._))
                .ReturnsLazily(() => Generators.FakeItem().Generate());
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
            return new GetItemCommand(this.id, this.retrieveChildren);
        }
    }
}
