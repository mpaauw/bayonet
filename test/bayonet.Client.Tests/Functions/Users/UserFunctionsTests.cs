using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace bayonet.Client.Tests.Functions.Users
{
    public class UserFunctionsTests
    {
        private readonly UserFunctionsFixture fixture;
        private readonly Faker faker;

        public UserFunctionsTests()
        {
            this.fixture = new UserFunctionsFixture();
            this.faker = new Faker();
        }

        [Fact]
        public async Task Request_For_User_Should_Return_Valid_Result()
        {
            string id = this.faker.Lorem.Word();
            var result = await this.fixture
                .WithId(id)
                .ExecuteGetUser();
            Assert.Equal(id, result.Value.Id);
        }

        [Fact]
        public async Task Request_For_Updated_Users_Should_Return_Valid_Result()
        {
            int count = this.faker.Random.Int(1, 10);
            var result = await this.fixture
                .WithCount(count)
                .ExecuteGetUpdatedUsers();
            Assert.Equal(count, result.Value.Count());
        }
    }
}
