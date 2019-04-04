using bayonet.Client.Functions.Items;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Tests.Common;
using Bogus;
using FakeItEasy;
using Flurl.Http;
using Flurl.Http.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Client.Tests.Functions.Items
{
    public class ItemFunctionsFixture
    {
        private readonly IFlurlClient flurlClient;
        private readonly IItemFunctions itemFunctions;
        private readonly Faker faker;
        private string id;
        private int count;

        public ItemFunctionsFixture()
        {
            this.flurlClient = new FlurlClient("https://example.com");
            this.itemFunctions = new ItemFunctions(flurlClient);
            this.faker = new Faker();
        }

        public ItemFunctionsFixture WithId(string id = null)
        {
            this.id = (id is null) ? this.faker.Lorem.Word() : id;
            return this;
        }

        public ItemFunctionsFixture WithCount(int count = -1)
        {
            this.count = (count == -1) ? this.faker.Random.Int(1, 10) : count;
            return this;
        }

        public async Task<Result<Item>> ExecuteGetItem()
        {
            using (var httpTest = new HttpTest())
            {
                var resultValue = Generators.FakeItem(this.id).Generate();
                httpTest.RespondWithJson(resultValue);
                return await this.itemFunctions.GetItem(this.id);
            }
        }

        public async Task<Result<Item>> ExecuteGetMaxItem()
        {
            using (var httpTest = new HttpTest())
            {
                var resultValue = Generators.FakeItem().Generate();
                httpTest.RespondWithJson(resultValue);
                return await this.itemFunctions.GetMaxItem();
            }
        }

        public async Task<Result<IEnumerable<Item>>> ExecuteGetUpdatedItems()
        {
            using (var httpTest = new HttpTest())
            {
                var resultValue = Generators.FakeItems(this.count);
                httpTest.RespondWithJson(resultValue);
                return await this.itemFunctions.GetUpdatedItems(this.count);
            }
        }
    }
}
