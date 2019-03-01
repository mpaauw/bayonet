using AndyC.Patterns.Commands;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Api.Commands.Users
{
    public class GetUserCommand : IFunction<Result<User>>
    {
        private readonly string id;

        public GetUserCommand(string id)
        {
            this.id = id;
        }

        public class Handler : IFunctionHandlerAsync<GetUserCommand, Result<User>>
        {
            private readonly IWebService webService;

            public Handler(IWebService webService)
            {
                this.webService = webService;
            }

            public async Task<Result<User>> ExecuteAsync(GetUserCommand function)
            {
                try
                {
                    var user = await this.webService.GetContentAsync<User>(Constants.UserEndpoint.Replace(Constants.Bayonet, function.id));
                    return new Result<User>()
                    {
                        Value = user
                    };
                }
                catch (Exception ex)
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
}
