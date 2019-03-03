using AndyC.Patterns.Commands;
using bayonet.Api.Commands.Items;
using bayonet.Api.Commands.Stories;
using bayonet.Api.Commands.Users;
using bayonet.Core.Common;
using bayonet.Core.Models;
using bayonet.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace bayonet.Api.Patterns
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IDictionary<Type, Func<ICommandHandler>> factory;

        public CommandHandlerFactory(IWebService webService)
        {
            this.factory = new Dictionary<Type, Func<ICommandHandler>>()
            {
                { typeof(IFunctionHandlerAsync<GetItemCommand, Result<Item>>), () => new GetItemCommand.Handler(webService) },
                { typeof(IFunctionHandlerAsync<GetMaxItemCommand, Result<Item>>), () => new GetMaxItemCommand.Handler(webService) },
                { typeof(IFunctionHandlerAsync<GetUpdatedItemsCommand, Result<IEnumerable<Item>>>), () => new GetUpdatedItemsCommand.Handler(webService) },
                { typeof(IFunctionHandlerAsync<GetStoriesCommand, Result<IEnumerable<Item>>>), () => new GetStoriesCommand.Handler(webService) },
                { typeof(IFunctionHandlerAsync<GetUpdatedUsersCommand, Result<IEnumerable<User>>>), () => new GetUpdatedUsersCommand.Handler(webService) },
                { typeof(IFunctionHandlerAsync<GetUserCommand, Result<User>>), () => new GetUserCommand.Handler(webService) }
            };
        }

        public ICommandHandler Create(Type handlerType)
        {
            Console.WriteLine($"Creating Command Handler of type: [{handlerType.FullName}]");
            return this.factory[handlerType]();
        }

        public void Release(ICommandHandler handler)
        {
        }
    }
}
