using AndyC.Patterns.Commands;
using bayonet.Api.Commands.Items;
using bayonet.Core.Common;
using bayonet.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bayonet.Api.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private readonly ICommandRouter commandRouter;

        public ItemsController(ICommandRouter commandRouter)
        {
            this.commandRouter = commandRouter;
        }

        /// <summary>
        /// Retrieves an Item given it's Id.
        /// </summary>
        /// <param name="id">String value representing the Id of the Item to retrieve.</param>
        /// <returns>An Item wrapped in a Result object.</returns>
        [HttpGet("{id}")]
        public async Task<Result<Item>> GetItem([FromRoute] string id)
        {
            var command = new GetItemCommand(id);
            return await this.commandRouter.ExecuteFunctionAsync(command);
        }

        /// <summary>
        /// Retrieves the current max Item.
        /// </summary>
        /// <returns>An Item wrapped in a Result object.</returns>
        [HttpGet("Max")]
        public async Task<Result<Item>> GetMaxItem()
        {
            var command = new GetMaxItemCommand();
            return await this.commandRouter.ExecuteFunctionAsync(command);
        }

        /// <summary>
        /// Retrieves a number of recently-updated Items.
        /// </summary>
        /// <param name="count">The number of recently-updated Items to retrieve.</param>
        /// <returns>A collection of Items wrapped in a Result object.</returns>
        [HttpGet("Updates/{count}")]
        public async Task<Result<IEnumerable<Item>>> GetUpdatedItems([FromRoute] int count)
        {
            var command = new GetUpdatedItemsCommand(count);
            return await this.commandRouter.ExecuteFunctionAsync(command);
        }

    }
}
