using bayonet.Api.Commands.Items;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Data;
using bayonet.Tests.Common;
using FakeItEasy;
using System;
using System.Threading.Tasks;

namespace bayonet.Api.Tests.Items
{
    public class GetMaxItemCommandFixture
    {
        private readonly IWebService webService;

        public GetMaxItemCommandFixture()
        {
            this.webService = A.Fake<IWebService>();
        }

        public GetMaxItemCommandFixture WithWebServiceGetContentAsyncExceptionEncountered()
        {
            A.CallTo(() => this.webService.GetContentAsync<Item>(A<string>._))
                .Throws(new Exception());
            return this;
        }

        public GetMaxItemCommandFixture WithValidWebServiceGetContentAsyncResponse()
        {
            var item = Generators.FakeItem().Generate();
            A.CallTo(() => this.webService.GetContentAsync<Item>(A<string>._))
                .Returns(item);
            return this;
        }

        public async Task<Result<Item>> ExecuteCommandUnderTest()
        {
            var command = GetCommandUnderTest();
            var handler = new GetMaxItemCommand.Handler(this.webService);
            return await handler.ExecuteAsync(command);
        }

        private GetMaxItemCommand GetCommandUnderTest()
        {
            return new GetMaxItemCommand();
        }
    }
}
