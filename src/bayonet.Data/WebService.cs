using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace bayonet.Data
{
    public class WebService : IWebService
    {
        private readonly HttpClient client;

        public WebService()
        {
            this.client = new HttpClient();
        }

        public async Task<T> GetContentAsync<T>(string endpoint)
        {
            T content = default(T);
            HttpResponseMessage response = await this.client.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsAsync<T>();
            }
            else
            {
                throw new Exception("Unable to retrieve content.");
            }
            return content;
        }
    }
}
