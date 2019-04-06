using bayonet.Core.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace bayonet.Client.Tests.Functions.Stories
{
    public class StoryFunctionsTests
    {
        private readonly StoryFunctionsFixture fixture;
        private readonly Faker faker;

        public StoryFunctionsTests()
        {
            this.fixture = new StoryFunctionsFixture();
            this.faker = new Faker();
        }

        [Fact]
        public async Task Get_Stories_Should_Return_Story_Items()
        {
            var result = await this.fixture
                .WithStoryType()
                .WithCount()
                .ExecuteGetStories();
            var actualTypes = result.Value.ToList().Select(x => x.Type).Distinct().ToList();
            Assert.True(actualTypes.Count == 1);
            Assert.Equal(ItemType.Story, actualTypes[0]);
        }

        [Fact]
        public async Task Get_Stories_Should_Return_Valid_Result()
        {
            int count = this.faker.Random.Int(1, 10);
            var result = await this.fixture
                .WithStoryType()
                .WithCount(count)
                .ExecuteGetStories();
            Assert.Equal(count, result.Value.Count());
        }
    }
}
