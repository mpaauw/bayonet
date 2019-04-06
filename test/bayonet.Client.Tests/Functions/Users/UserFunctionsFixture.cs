using bayonet.Client.Functions.Users;
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

namespace bayonet.Client.Tests.Functions.Users
{
    public class UserFunctionsFixture
    {
        private readonly IFlurlClient flurlClient;
        private readonly IUserFunctions userFunctions;
        private readonly Faker faker;
        private string id;
        private int count;

        public UserFunctionsFixture()
        {
            this.flurlClient = new FlurlClient("https://example.comhttps://example.com");
            this.userFunctions = new UserFunctions(this.flurlClient);
            this.faker = new Faker();
        }

        public UserFunctionsFixture WithId(string id = null)
        {
            this.id = (id is null) ? this.faker.Lorem.Word() : id;
            return this;
        }

        public UserFunctionsFixture WithCount(int count = -1)
        {
            this.count = (count == -1) ? this.faker.Random.Int(1, 10) : count;
            return this;
        }

        public async Task<Result<User>> ExecuteGetUser()
        {
            using (var httpTest = new HttpTest())
            {
                var response = new Result<User>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Value = Generators.FakeUser(this.id).Generate()
                };
                httpTest.RespondWithJson(response);
                return await this.userFunctions.GetUser(this.id);
            }
        }

        public async Task<Result<IEnumerable<User>>> ExecuteGetUpdatedUsers()
        {
            using (var httpTest = new HttpTest())
            {
                var response = new Result<IEnumerable<User>>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Value = Generators.FakeUsers(this.count)
                };
                httpTest.RespondWithJson(response);
                return await this.userFunctions.GetUpdatedUsers(this.count);
            }
        }
    }
}
