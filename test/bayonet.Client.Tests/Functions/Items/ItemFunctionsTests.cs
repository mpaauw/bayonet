using Bogus;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace bayonet.Client.Tests.Functions.Items
{
    public class ItemFunctionsTests
    {
        private readonly ItemFunctionsFixture fixture;
        private readonly Faker faker;

        public ItemFunctionsTests()
        {
            this.fixture = new ItemFunctionsFixture();
            this.faker = new Faker();
        }

        [Fact]
        public async Task Request_For_Item_Should_Return_Valid_Result()
        {
            string id = this.faker.Lorem.Word();
            var result = await this.fixture
                .WithId(id)
                .ExecuteGetItem();
            Assert.Equal(id, result.Value.Id);
        }

        [Fact]
        public async Task Request_For_Max_Item_Should_Return_Valid_Result()
        {
            var result = await this.fixture
                .ExecuteGetMaxItem();
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task Request_For_Updated_Items_Should_Return_Valid_Result()
        {
            int count = this.faker.Random.Int(1, 10);
            var result = await this.fixture
                .WithCount(count)
                .ExecuteGetUpdatedItems();
            Assert.Equal(count, result.Value.Count());
        }
    }
}
