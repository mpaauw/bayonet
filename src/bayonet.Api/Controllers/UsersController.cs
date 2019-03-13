using AndyC.Patterns.Commands;
using bayonet.Api.Commands.Users;
using bayonet.Core.Common;
using bayonet.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bayonet.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ICommandRouter commandRouter;

        public UsersController(ICommandRouter commandRouter)
        {
            this.commandRouter = commandRouter;
        }

        /// <summary>
        /// Retrieves a User, given that User's Id.
        /// </summary>
        /// <param name="id">Id of the User to retrieve.</param>
        /// <returns>A User wrapped in a Result object.</returns>
        [HttpGet("{id}")]
        public async Task<Result<User>> GetUser([FromRoute] string id)
        {
            var command = new GetUserCommand(id);
            return await this.commandRouter.ExecuteFunctionAsync(command);
        }

        /// <summary>
        /// Retrieves a number of recently-updated Users.
        /// </summary>
        /// <param name="count">The number of recently-updated Users to retrieve.</param>
        /// <returns>A collection of Users wrapped in a Result object.</returns>
        [HttpGet("Updates/{count}")]
        public async Task<Result<IEnumerable<User>>> GetUpdatedUsers([FromRoute] int count)
        {
            var command = new GetUpdatedUsersCommand(count);
            return await this.commandRouter.ExecuteFunctionAsync(command);
        }
    }
}
