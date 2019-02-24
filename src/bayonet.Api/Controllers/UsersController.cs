using bayonet.Api.Commands;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IWebService webService;

        public UsersController(IWebService webService)
        {
            this.webService = webService;
        }

        [HttpGet("{id}")]
        public async Task<Result<User>> GetUser([FromRoute] string id)
        {
            var command = new GetUserCommand(this.webService, id);
            return await command.ExecuteAsync();
        }
    }
}
