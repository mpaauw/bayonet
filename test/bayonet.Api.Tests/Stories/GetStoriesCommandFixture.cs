using bayonet.Api.Commands.Stories;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Data;
using bayonet.Tests.Common;
using Bogus;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bayonet.Api.Tests.Stories
{
    public class GetStoriesCommandFixture
    {
        private readonly IWebService webService;
        private readonly Faker faker;

        private string storyType;
        private int count;

        public GetStoriesCommandFixture()
        {
            this.webService = A.Fake<IWebService>();
            this.faker = new Faker();
        }

        public GetStoriesCommandFixture WithInvalidStoryType()
        {
            this.storyType = this.faker.Lorem.Word();
            return this;
        }

        public GetStoriesCommandFixture WithValidStoryType()
        {
            var storyTypes = Enum.GetValues(typeof(StoryType));
            this.storyType = storyTypes.GetValue(new Random().Next(storyTypes.Length)).ToString();
            return this;
        }

        public GetStoriesCommandFixture WithInvalidCount()
        {
            int[] badData = new int[] { -1, 0 };
            this.count = badData[this.faker.Random.Int(0, 1)];
            return this;
        }

        public GetStoriesCommandFixture WithValidCount()
        {
            this.count = this.faker.Random.Int(1, 10);
            return this;
        }

        public GetStoriesCommandFixture WithWebServiceGetContentAsyncStoryIdsExceptionEncountered()
        {
            A.CallTo(() => this.webService.GetContentAsync<IEnumerable<string>>(A<string>._))
                .Throws(new Exception());
            return this;
        }

        public GetStoriesCommandFixture WithValidWebServiceGetContentAsyncStoryIdsResponse()
        {
            var storyIds = this.faker.Lorem.Words();
            A.CallTo(() => this.webService.GetContentAsync<IEnumerable<string>>(A<string>._))
                .Returns(storyIds);
            return this;
        }

        public GetStoriesCommandFixture WithValidWebServiceGetContentAsyncItemResponse()
        {
            var item = Generators.FakeItem().Generate();
            A.CallTo(() => this.webService.GetContentAsync<Item>(A<string>._))
                .Returns(item);
            return this;
        }

        public async Task<Result<IEnumerable<Item>>> ExecuteCommandUnderTest()
        {
            var command = GetCommandUnderTest();
            var handler = new GetStoriesCommand.Handler(this.webService);
            return await handler.ExecuteAsync(command);
        }

        private GetStoriesCommand GetCommandUnderTest()
        {
            return new GetStoriesCommand(
                this.storyType,
                this.count);
        }
    }
}
