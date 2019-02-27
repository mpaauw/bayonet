﻿using bayonet.Api.Commands;
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

        /// <summary>
        /// Retrieves a number of Stories, based on type.
        /// </summary>
        /// <param name="storyType">The type of Stories to retrieve. Can consist of Top, New, Best, Ask, Show, or Job.</param>
        /// <param name="count">The number of Stories to retrieve.</param>
        /// <returns>A collection of Stories wrapped in a Result object.</returns>
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
