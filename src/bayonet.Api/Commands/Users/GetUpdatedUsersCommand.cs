﻿using AndyC.Patterns.Commands;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace bayonet.Api.Commands.Users
{
    public class GetUpdatedUsersCommand : IFunction<Result<IEnumerable<User>>>
    {
        private readonly int count;

        public GetUpdatedUsersCommand(int count)
        {
            this.count = count;
        }

        public class Handler : IFunctionHandlerAsync<GetUpdatedUsersCommand, Result<IEnumerable<User>>>
        {
            private readonly IWebService webService;

            public Handler(IWebService webService)
            {
                this.webService = webService;
            }

            public async Task<Result<IEnumerable<User>>> ExecuteAsync(GetUpdatedUsersCommand function)
            {
                try
                {
                    if(!BayonetHelper.ValidateCount(function.count))
                    {
                        return new Result<IEnumerable<User>>()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            IsError = true,
                            ErrorMessage = "Invalid count."
                        };
                    }
                    var updates = await this.webService.GetContentAsync<Updates>(Constants.UpdatesEndpoint);
                    var updatedUsers = new List<User>();
                    foreach (var userId in updates.Profiles.Take(function.count))
                    {
                        var user = await this.webService.GetContentAsync<User>(Constants.UserEndpoint.Replace(Constants.Bayonet, userId));
                        updatedUsers.Add(user);
                    }
                    return new Result<IEnumerable<User>>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Value = updatedUsers
                    };
                }
                catch (Exception ex)
                {
                    return new Result<IEnumerable<User>>()
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        IsError = true,
                        ErrorMessage = ex.Message
                    };
                }
            }
        }
    }
}
