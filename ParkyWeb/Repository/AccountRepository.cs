using Newtonsoft.Json;
using ParkyWeb.Models;
using ParkyWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParkyWeb.Repository
{
    public class AccountRepository : Repository<User>, IAccountRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public AccountRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<User> LoginAsync(string url, User objToCreate)
        {
            //Creating the request, here decide what type of request I need, Post, Get, Put...
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (objToCreate != null)
            {
                //Building the content to send, equivalent to a user writing the content of the object in JSON
                request.Content = new StringContent(JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
            }
            else
            {
                return new User();
            }

            //Creating the client, it is like creating a virtual user who is going to send the request
            var client = _clientFactory.CreateClient();
            //Client send the request and the result is saved by response variable
            HttpResponseMessage response = await client.SendAsync(request);
            //If the respose received is good return true, if not return false
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var JSONstr = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(JSONstr);
            }
            else
            {
                return new User();
            }
        }

        public async Task<bool> RegisterAsync(string url, User objToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (objToCreate != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
