using CustomerApplicationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerApplicationWeb.Client
{
    public interface IClient
    {
        Task<HttpResponseMessage> GetClient();

        HttpResponseMessage PostClient(CustomerDTO customer);

        HttpResponseMessage PutClient(CustomerDTO customer);

        HttpResponseMessage DeleteClient(long id);
    }
}
