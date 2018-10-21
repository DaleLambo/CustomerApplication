using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CustomerApplicationWeb.Client;
using CustomerApplicationWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CustomerApplicationWeb.Controllers
{
    // Note-JsonConvert.SerializeObject & JsonConvert.DeserializeObject are used to serialize and deserialize the data.
    public class CustomersController : Controller
    {
        private IClient _client;

        public CustomersController(IClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Method gets all customers data from the DB. It's defined as asynchronous
        /// to return the view for the index and customer list values.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            List<CustomerDTO> dto = new List<CustomerDTO>();

            var res = await _client.GetClient();
            
            //Checking the response is successful or not which is sent using HttpClient    
            if (res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api     
                var result = res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list    
                dto = JsonConvert.DeserializeObject<List<CustomerDTO>>(result);

            }
            //returning the employee list to view    
            return View(dto);
        }

        /// <summary>
        /// Method simply returns it's corresponding View.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Method uses Post attribute to be invoked on Post requests. Method handles adding
        /// new data to the DB aslong as the modelstate is valid on the posts.
        /// </summary>
        /// <param name="customer"> Customer object</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,Surname,Email,Password")] CustomerDTO customer)
        {
            // Check modelstate
            if (ModelState.IsValid)
            {
                var res = _client.PostClient(customer);

                // Check for successful response
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(customer);
        }

        /// <summary>
        /// Method use id for a identifier parameter. If id is null nothing is found while
        /// if its not null return the corresponding view data.
        /// </summary>
        /// <param name="id">Identifier Parameter</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(long? id)
        {
            // Check id isn't null
            if (id == null)
            {
                return NotFound();
            }

            List<CustomerDTO> dto = new List<CustomerDTO>();

            var res = await _client.GetClient();

            // Check for successful response
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<CustomerDTO>>(result);
            }

            var customer = dto.SingleOrDefault(m => m.Id == id);

            // Check customer isn't null
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        /// <summary>
        /// Method updates the corresponding data to the database using the id.
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="customer">Customer object</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id, FirstName, Surname, Email, Password")] CustomerDTO customer)
        {
            // Check id
            if (id != customer.Id)
            {
                return NotFound();
            }

            // Check modelstate
            if (ModelState.IsValid)
            {
                var res = _client.PutClient(customer);

                // Check for successful response
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(customer);
        }

        /// <summary>
        /// Method takes the id as the parameter. If the id is null, it returns nothing. 
        /// If it isn't null, returns the corresponding View data.
        /// </summary>
        /// <param name="id">Id Identifier</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(long? id)
        {
            // Check id isn't null
            if (id == null)
            {
                return NotFound();
            }

            List<CustomerDTO> dto = new List<CustomerDTO>();

            var res = await _client.GetClient();

            // Check for successful response
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<CustomerDTO>>(result);
            }

            var customer = dto.SingleOrDefault(m => m.Id == id);

            // Check customer isn't null
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        /// <summary>
        ///  Method deletes the corresponding data to the database using the id.
        /// </summary>
        /// <param name="id">customer Idendifier</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var res = _client.DeleteClient(id);

            // Check for successful response
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}