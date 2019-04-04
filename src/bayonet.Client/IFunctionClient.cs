using bayonet.Client.Functions.Items;
using bayonet.Client.Functions.Stories;
using bayonet.Client.Functions.Users;

namespace bayonet.Client
{
    public interface IFunctionClient
    {
        /// <summary>
        /// Provides access to Item-related APIs.
        /// </summary>
        IItemFunctions Items { get; }

        /// <summary>
        /// Provides access to Story-related APIs.
        /// </summary>
        IStoryFunctions Stories { get; }

        /// <summary>
        /// Provides access to User-related APIs.
        /// </summary>
        IUserFunctions Users { get; }
    }
}
