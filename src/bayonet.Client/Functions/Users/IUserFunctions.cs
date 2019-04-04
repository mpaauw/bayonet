using bayonet.Core.Common;
using bayonet.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bayonet.Client.Functions.Users
{
    public interface IUserFunctions
    {
        Task<Result<User>> GetUser(string id);

        Task<Result<IEnumerable<User>>> GetUpdatedUsers(int count);
    }
}
