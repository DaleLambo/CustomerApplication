using CustomerApplicationApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApplicationApi.Services
{
    // Class computes functionality of behaviours needed for the API
    public class CustomerService : ICustomerService<Customer, long>
    {
        ApplicationContext _context; // Used to get access to DB/In-memory data.
        public CustomerService(ApplicationContext context)
        {
            _context = context; 

            // Comment out If using SQL Database, This was for In-memory DB for testing
            if (_context.Customers.Count() <= 0)
            {
                // If customer collection is empty make new customer
                // This wont delete all customers.
                _context.Customers.Add(new Customer { FirstName = "Dale", Surname = "Lambert", Email = "DaleLambo@outlook.co.uk", Password = "Lambo123" });
                _context.Customers.Add(new Customer { FirstName = "John", Surname = "Dickinson", Email = "JDicko@outlook.co.uk", Password = "Dicko123" });
                _context.SaveChanges();
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            var customers = _context.Customers.ToList();
            return customers;
        }

        public Customer Get(long id)
        {
            var customer = _context.Customers.FirstOrDefault(b => b.Id == id);
            return customer;
        }

        public long Add(Customer customer)
        {
            _context.Customers.Add(customer);
            long customerID = _context.SaveChanges();
            return customerID;
        }
        
        public long Delete(long id)
        {
            int customerID = 0;
            var customer = _context.Customers.FirstOrDefault(b => b.Id == id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                customerID = _context.SaveChanges();
            }
            return customerID;
        }

        public long Update(long id, Customer pCustomer)
        {
            long customerID = 0;
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                customer.FirstName = pCustomer.FirstName;
                customer.Surname = pCustomer.Surname;
                customer.Email = pCustomer.Email;
                customer.Password = pCustomer.Password;

                customerID = _context.SaveChanges();
            }
            return customerID;
        }
    }
}
