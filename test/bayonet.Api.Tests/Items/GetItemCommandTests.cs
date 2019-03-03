using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace bayonet.Api.Tests.Items
{
    public class GetItemCommandTests
    {
        private readonly GetItemCommandFixture fixture;

        public GetItemCommandTests()
        {
            this.fixture = new GetItemCommandFixture();
        }

        [Fact]
        public async Task Invalid_Request_Should_Return_Bad_Request()
        {
            var result = await this.fixture
                .WithInvalidId()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Encountering_Exception_Should_Return_Server_Error()
        {
            var result = await this.fixture
                .WithValidId()
                .WithWebServiceGetContentAsyncExceptionEncountered()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task Valid_Request_Should_Return_Ok()
        {
            var result = await this.fixture
                .WithValidId()
                .WithValidWebServiceGetContentAsyncResponse()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
