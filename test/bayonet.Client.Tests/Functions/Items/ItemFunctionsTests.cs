using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
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

        // ensure get item returns expected data
        [Fact]
        public async Task Request_For_Item_Should_Return_Item()
        {
            string id = this.faker.Lorem.Word();
            var result = await this.fixture
                .WithId(id)
                .ExecuteGetItem();
            Assert.Equal(id, result.Value.Id);
        }

        // ensure getmaxitem returns expected data

        // ensure getupdateditems returns expected data
    }
}
