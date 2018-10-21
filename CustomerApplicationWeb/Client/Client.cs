using CustomerApplicationWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApplicationWeb.Client
{
    // CustomerClientAPI InitilizeClient method Initializes the HttpClient Object.
    // This is needed to access the API endpoints.
    public class CustomerClientAPI
    {
        private string _apiBaseURI = "http://localhost:10542";
        public HttpClient InitializeClient()
        {
            var client = new HttpClient();
            // Passes base url service.    
            client.BaseAddress = new Uri(_apiBaseURI);

            client.DefaultRequestHeaders.Clear();
            // Defines data request format.   
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }

    // Client class computes the functionality of behaviours for the API endpoints from the HttpClient Object.
    public class Client : IClient
    {
        CustomerClientAPI _customerAPI = new CustomerClientAPI();

        public async Task<HttpResponseMessage> GetClient()
        {
            HttpClient client = _customerAPI.InitializeClient();
            HttpResponseMessage response = await client.GetAsync("api/customers");
            return response;
        }

        public HttpResponseMessage PostClient(CustomerDTO customer)
        {
            HttpClient client = _customerAPI.InitializeClient();
            var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("api/customers", content).Result;
            return response;
        }

        public HttpResponseMessage PutClient(CustomerDTO customer)
        {
            HttpClient client = _customerAPI.InitializeClient();
            var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync("api/customers", content).Result;
            return response;
        }

        public HttpResponseMessage DeleteClient(long id)
        {
            HttpClient client = _customerAPI.InitializeClient();
            HttpResponseMessage response = client.DeleteAsync($"api/customers/{id}").Result;
            return response;
        }
    }
}
