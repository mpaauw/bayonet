using bayonet.Client.Functions.Items;
using bayonet.Client.Functions.Stories;
using bayonet.Client.Functions.Users;
using Flurl.Http;

namespace bayonet.Client
{
    public class FunctionClient : IFunctionClient
    {
        public FunctionClient(IFlurlClient flurlClient)
        {
            this.Items = new ItemFunctions(flurlClient);
            this.Stories = new StoryFunctions(flurlClient);
            this.Users = new UserFunctions(flurlClient);
        }

        /// <inheritdoc />
        public IItemFunctions Items { get; }

        /// <inheritdoc />
        public IStoryFunctions Stories { get; }

        /// <inheritdoc />
        public IUserFunctions Users { get; }
    }
}
