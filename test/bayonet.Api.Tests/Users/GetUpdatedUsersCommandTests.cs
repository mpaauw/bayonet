using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace bayonet.Api.Tests.Users
{
    public class GetUpdatedUsersCommandTests
    {
        private readonly GetUpdatedUsersCommandFixture fixture;

        public GetUpdatedUsersCommandTests()
        {
            this.fixture = new GetUpdatedUsersCommandFixture();
        }

        [Fact]
        public async Task Invalid_Count_Should_Return_Bad_Request()
        {
            var result = await this.fixture
                .WithInvalidCount()
                .WithValidWebServiceGetContentAsyncUpdatesResponse()
                .WithValidWebServiceGetContentAsyncUserResponse()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Encountering_Exception_Should_Return_Server_Error()
        {
            var result = await this.fixture
                .WithValidCount()
                .WithWebServiceGetContentAsyncUpdatesExceptionEncountered()
                .WithValidWebServiceGetContentAsyncUserResponse()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task Valid_Request_Should_Return_Ok()
        {
            var result = await this.fixture
                .WithValidCount()
                .WithValidWebServiceGetContentAsyncUpdatesResponse()
                .WithValidWebServiceGetContentAsyncUserResponse()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
