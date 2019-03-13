using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace bayonet.Api.Tests.Users
{
    public class GetUserCommandTests
    {
        private readonly GetUserCommandFixture fixture;

        public GetUserCommandTests()
        {
            this.fixture = new GetUserCommandFixture();
        }

        [Fact]
        public async Task Invalid_Id_Should_Return_Bad_Request()
        {
            var result = await this.fixture
                .WithInvalidId()
                .WithValidWebServiceGetContentAsyncUserResponse()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Encountering_Exception_Should_Return_Server_Error()
        {
            var result = await this.fixture
                .WithValidId()
                .WithWebServiceGetContentAsyncUserExceptionEncountered()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task Valid_Request_Should_Return_Ok()
        {
            var result = await this.fixture
                .WithValidId()
                .WithValidWebServiceGetContentAsyncUserResponse()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

    }
}
