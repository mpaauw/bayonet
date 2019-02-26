using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Core.Patterns;
using bayonet.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Api.Commands.Users
{
    public class GetUserCommand : Command<Result<User>>
    {
        private readonly IWebService webService;
        private readonly string id;

        public GetUserCommand(IWebService webService, string id)
        {
            this.webService = webService;
            this.id = id;
        }

        public override async Task<Result<User>> ExecuteAsync()
        {
            try
            {
                var user = await this.webService.GetContentAsync<User>(Constants.UserEndpoint.Replace(Constants.Bayonet, this.id));
                return new Result<User>()
                {
                    Value = user
                };
            }
            catch(Exception ex)
            {
                return new Result<User>()
                {
                    IsError = true,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
