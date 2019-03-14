using bayonet.Core.Common;
using bayonet.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Client.Functions.Stories
{
    public interface IStoryFunctions
    {
        Task<Result<IEnumerable<Item>>> GetStories(string storyType, int count);
    }
}
