using bayonet.Core.Models;
using System.Net;
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

        [Fact]
        public async Task Requesting_Nested_Retrieval_Should_Return_Nested_Items_Result()
        {
            var result = await this.fixture
                .WithValidId()
                .WithRetrieveChildren()
                .WithValidWebServiceGetContentAsyncResponse()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(VerifyChildrenRecursively(result.Value));
        }

        private bool VerifyChildrenRecursively(Item item)
        {
            if(item.Kids is null || item.Kids.Length < 1)
            {
                return item.Children is null;
            }
            bool valid = true;
            valid = (item.Children.Length == item.Kids.Length) ? true : false;
            foreach(var child in item.Children)
            {
                valid = (child != null) ? true : false;
                valid = VerifyChildrenRecursively(child);
            }
            return valid;
        }
    }
}
