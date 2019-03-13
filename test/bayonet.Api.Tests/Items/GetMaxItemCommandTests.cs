using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace bayonet.Api.Tests.Items
{
    public class GetMaxItemCommandTests
    {
        private readonly GetMaxItemCommandFixture fixture;

        public GetMaxItemCommandTests()
        {
            this.fixture = new GetMaxItemCommandFixture();
        }

        [Fact]
        public async Task Encountering_Exception_Should_Return_Server_Error()
        {
            var result = await this.fixture
                .WithWebServiceGetContentAsyncExceptionEncountered()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task Valid_Request_Should_Return_Ok()
        {
            var result = await this.fixture
                .WithValidWebServiceGetContentAsyncResponse()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
