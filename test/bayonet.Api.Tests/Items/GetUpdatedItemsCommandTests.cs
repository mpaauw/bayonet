using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace bayonet.Api.Tests.Items
{
    public class GetUpdatedItemsCommandTests
    {
        private readonly GetUpdatedItemsCommandFixture fixture;

        public GetUpdatedItemsCommandTests()
        {
            this.fixture = new GetUpdatedItemsCommandFixture();
        }

        [Fact]
        public async Task Invalid_Count_Should_Return_Bad_Request()
        {
            var result = await this.fixture
                .WithInvalidCount()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Encountering_Exception_Should_Return_Server_Error()
        {
            var result = await this.fixture
                .WithValidCount()
                .WithWebServiceGetContentAsyncUpdatesExceptionEncountered()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task Valid_Request_Should_Return_Ok()
        {
            var result = await this.fixture
                .WithValidCount()
                .WithValidWebServiceGetContentAsyncUpdatesResponse()
                .WithValidWebServiceGetContentAsyncItemResponse()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
