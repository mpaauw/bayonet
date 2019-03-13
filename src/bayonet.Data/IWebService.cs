using System.Threading.Tasks;

namespace bayonet.Data
{
    public interface IWebService
    {
        Task<T> GetContentAsync<T>(string endpoint);
    }
}
