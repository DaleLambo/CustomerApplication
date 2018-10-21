using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApplicationApi.Models;
using CustomerApplicationApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerApplicationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        // Constructor Injection - Dependency Injection
        private ICustomerService<Customer, long> _customerService;
        public CustomersController(ICustomerService<Customer, long> customerService)
        {
            _customerService = customerService;
        }

        // GET api/Customers/Get
        /// <summary>
        /// Gets all Customers.
        /// </summary>
        /// <returns>OkResult</returns>
        [HttpGet]        
        public ActionResult<IEnumerable<Customer>> Get()
        {
            var customers = _customerService.GetAll();

            return Ok(customers);
        }

        // GET api/Customers/Get/2
        /// <summary>
        /// Gets customer based on Id parameter.
        /// </summary>
        /// <param name="id">Identifier parameter id</param>
        /// <returns>OkResult</returns>
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = _customerService.Get(id);

            // Checks customer isn't Null
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST api/Customers/Create
        /// <summary>
        /// Creates new Customers.
        /// </summary>
        /// <param name="customer">New customer parameter</param>
        /// <returns>CreatedActionResult</returns>
        [HttpPost]
        public ActionResult Post([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Adds new customer
            _customerService.Add(customer);
            return CreatedAtAction("Get", new { id = customer.Id }, customer);
        }

        // POST api/Customers/Edit
        /// <summary>
        /// Updates or edits existing customer.
        /// </summary>
        /// <param name="customer">Existing customer paramater</param>
        /// <returns>CreatedActionResult</returns>
        [HttpPut]
        public ActionResult Put([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Gets customer data for editing
            _customerService.Update(customer.Id, customer);
            return CreatedAtAction("Get", new { id = customer.Id }, customer);
        }

        // DELETE api/Customers/Delete/2
        /// <summary>
        /// Deletes customer based on Id parameter.
        /// </summary>
        /// <param name="id">Identifier parameter id</param>
        /// <returns>OkResult</returns>
        [HttpDelete("{id}")]
        public ActionResult<long> Delete(int id)
        {
            // Gets customer based on Id
            var customer = _customerService.Get(id);

            // Checks customer isn't Null
            if (customer == null)
            {
                return NotFound();
            }

            var customerId = _customerService.Delete(id);

            return Ok(customerId);
        }
    }
}
