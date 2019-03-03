using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace bayonet.Api.Tests.Stories
{
    public class GetStoriesCommandTests
    {
        private readonly GetStoriesCommandFixture fixture;

        public GetStoriesCommandTests()
        {
            this.fixture = new GetStoriesCommandFixture();
        }

        [Fact]
        public async Task Invalid_Story_Type_Should_Return_Bad_Request()
        {
            var result = await this.fixture
                .WithInvalidStoryType()
                .WithValidCount()
                .WithValidWebServiceGetContentAsyncStoryIdsResponse()
                .WithValidWebServiceGetContentAsyncItemResponse()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Invalid_Count_Should_Return_Bad_Request()
        {
            var result = await this.fixture
                .WithValidStoryType()
                .WithInvalidCount()
                .WithValidWebServiceGetContentAsyncStoryIdsResponse()
                .WithValidWebServiceGetContentAsyncItemResponse()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Encountering_Exception_Should_Return_Server_Error()
        {
            var result = await this.fixture
                .WithValidStoryType()
                .WithValidCount()
                .WithWebServiceGetContentAsyncStoryIdsExceptionEncountered()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task Valid_Request_Should_Return_Ok()
        {
            var result = await this.fixture
                .WithValidStoryType()
                .WithValidCount()
                .WithValidWebServiceGetContentAsyncStoryIdsResponse()
                .WithValidWebServiceGetContentAsyncItemResponse()
                .ExecuteCommandUnderTest();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
