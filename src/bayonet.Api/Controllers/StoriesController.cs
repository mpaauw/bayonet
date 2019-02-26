using bayonet.Api.Commands;
using bayonet.Api.Commands.Stories;
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
    public class StoriesController : Controller
    {
        private readonly IWebService webService;

        public StoriesController(IWebService webService)
        {
            this.webService = webService;
        }

        [HttpGet("{storyType}/{count}")]
        public async Task<Result<IEnumerable<Item>>> GetTopStories(
            [FromRoute] string storyType,
            [FromRoute] int count)
        {
            var command = new GetStoriesCommand(this.webService, storyType, count);
            return await command.ExecuteAsync();
        }
    }
}
