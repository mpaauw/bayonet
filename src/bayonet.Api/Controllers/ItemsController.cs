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
    public class ItemsController : Controller
    {
        private readonly IWebService webService;

        public ItemsController(IWebService webService)
        {
            this.webService = webService;
        }

        [HttpGet("{id}")]
        public async Task<Result<Item>> GetItem([FromRoute] int id)
        {
            var command = new GetItemCommand(this.webService, id);
            return await command.ExecuteAsync();
        }

        [HttpGet("Max")]
        public async Task<Result<Item>> GetMaxItem()
        {
            var command = new GetMaxItemCommand(this.webService);
            return await command.ExecuteAsync();
        }

    }
}
