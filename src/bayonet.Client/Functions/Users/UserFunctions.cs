
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using bayonet.Core.Common;
using bayonet.Core.Models;
using Flurl.Http;

namespace bayonet.Client.Functions.Users
{
    public class UserFunctions : IUserFunctions
    {
        private readonly IFlurlClient flurlClient;

        public UserFunctions(IFlurlClient flurlClient)
        {
            this.flurlClient = flurlClient;
        }

        public async Task<Result<User>> GetUser(string id)
        {
            return await this.flurlClient
                .Request(
                Constants.ApiSegment,
                Constants.UsersSegment,
                id)
                .GetJsonAsync<Result<User>>();
        }

        public async Task<Result<IEnumerable<User>>> GetUpdatedUsers(int count)
        {
            return await this.flurlClient
                .Request(
                Constants.ApiSegment,
                Constants.UsersSegment,
                Constants.UpdatesSegment,
                count)
                .GetJsonAsync<Result<IEnumerable<User>>>();
        }
    }
}
