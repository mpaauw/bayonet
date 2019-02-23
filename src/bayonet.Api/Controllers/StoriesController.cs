using bayonet.Api.Commands;
using bayonet.Core.Common;
using bayonet.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Api.Controllers
{
    [Route("api/[controller]")]
    public class StoriesController : Controller
    {
        [HttpGet("Top/{count}")]
        public async Task<Result<IEnumerable<Item>>> GetTopStories([FromRoute] int count)
        {
            var command = new GetTopStoriesCommand(count);
            return await command.ExecuteAsync();
        }
    }
}
