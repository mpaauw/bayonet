using bayonet.Client.Functions.Stories;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Tests.Common;
using Bogus;
using Flurl.Http;
using Flurl.Http.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Client.Tests.Functions.Stories
{
    public class StoryFunctionsFixture
    {
        private readonly IFlurlClient flurlClient;
        private readonly IStoryFunctions storyFunctions;
        private readonly Faker faker;
        private string storyType;
        private int count;

        public StoryFunctionsFixture()
        {
            this.flurlClient = new FlurlClient("https://example.com");
            this.storyFunctions = new StoryFunctions(this.flurlClient);
            this.faker = new Faker();
        }

        public StoryFunctionsFixture WithStoryType(string storyType = null)
        {
            var storyTypes = Enum.GetNames(typeof(StoryType));
            this.storyType = (storyType is null) ? storyTypes[this.faker.Random.Int(0, storyTypes.Length - 1)] : storyType;
            return this;
        }

        public StoryFunctionsFixture WithCount(int count = -1)
        {
            this.count = (count == -1) ? this.faker.Random.Int(1, 10) : count;
            return this;
        }

        public async Task<Result<IEnumerable<Item>>> ExecuteGetStories()
        {
            using (var httpTest = new HttpTest())
            {
                var response = new Result<IEnumerable<Item>>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Value = Generators.FakeItems(this.count)
                };
                httpTest.RespondWithJson(response);
                return await this.storyFunctions.GetStories(this.storyType, this.count);
            }
        }
    }
}
