using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bayonet.Core.Patterns
{
    public abstract class Command<TOutput>
    {
        public abstract Task<TOutput> ExecuteAsync();
    }
}
