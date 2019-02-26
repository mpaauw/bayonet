using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Core.Patterns;
using bayonet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Api.Commands.Users
{
    public class GetUpdatedUsersCommand : Command<Result<IEnumerable<User>>>
    {
        private readonly IWebService webService;
        private readonly int count;

        public GetUpdatedUsersCommand(IWebService webService, int count)
        {
            this.webService = webService;
            this.count = count;
        }

        public override async Task<Result<IEnumerable<User>>> ExecuteAsync()
        {
            try
            {
                var updates = await this.webService.GetContentAsync<Updates>(Constants.UpdatesEndpoint);
                var updatedUsers = new List<User>();
                foreach (var userId in updates.Profiles.Take(this.count))
                {
                    var getUserCommand = new GetUserCommand(this.webService, userId);
                    var getUserCommandResult = await getUserCommand.ExecuteAsync();
                    updatedUsers.Add(getUserCommandResult.Value);
                }
                return new Result<IEnumerable<User>>()
                {
                    Value = updatedUsers
                };
            }
            catch(Exception ex)
            {
                return new Result<IEnumerable<User>>()
                {
                    IsError = true,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
