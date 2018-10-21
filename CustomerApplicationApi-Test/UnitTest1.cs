using CustomerApplicationApi.Controllers;
using CustomerApplicationApi.Models;
using CustomerApplicationApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CustomerApplicationApi_Test
{
    public class CustomersControllerTest
    {
        #region GetAll Tests  
        [Fact]
        public void CustomersController_GetAll_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var mock = new Mock<ICustomerService<Customer, long>>();
            mock.Setup(service => service.GetAll()).Returns(new List<Customer> {
                new Customer { Id = 1, FirstName="Test1", Surname="Test1", Email = "Test1@Test.com", Password="Test123" },
                new Customer { Id = 2, FirstName="Test2", Surname="Test2", Email = "Test2@Test.com", Password="Test345" } });
            var controller = new CustomersController(mock.Object);

            // Act
            var okResult = controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void CustomersController_GetAll_WhenCalled_ReturnsAllItems()
        {
            // Arrange 
            var mock = new Mock<ICustomerService<Customer, long>>();
            mock.Setup(service => service.GetAll()).Returns(new List<Customer> {
                new Customer { Id = 1, FirstName="Test1", Surname="Test1", Email = "Test1@Test.com", Password="Test123" },
                new Customer { Id = 2, FirstName="Test2", Surname="Test2", Email = "Test2@Test.com", Password="Test345" } });
            var controller = new CustomersController(mock.Object);

            // Act
            var okResult = controller.Get().Result as OkObjectResult;
            var customers = Assert.IsType<List<Customer>>(okResult.Value);

            // Assert
            Assert.Equal(2, customers.Count);
        }
        #endregion

        #region GetById Tests
        [Fact]
        public void CustomersController_GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Arrange
            var mock = new Mock<ICustomerService<Customer, long>>();
            mock.Setup(service => service.Get(1)).Returns(new Customer { Id = 1, FirstName = "Test1", Surname = "Test1", Email = "Test1@Test.com", Password = "Test123" });
            var controller = new CustomersController(mock.Object);

            // Act
            var notFoundResult = controller.Get(3);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void CustomersController_GetById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var mock = new Mock<ICustomerService<Customer, long>>();
            mock.Setup(service => service.Get(1)).Returns(new Customer { Id = 1, FirstName = "Test1", Surname = "Test1", Email = "Test1@Test.com", Password = "Test123" });
            var controller = new CustomersController(mock.Object);

            // Act
            var OkResult = controller.Get(1);

            // Assert
            Assert.IsType<OkObjectResult>(OkResult.Result);
        }

        [Fact]
        public void CustomersController_GetById_ExistingIdPassed_ReturnsItem()
        {
            // Arrange
            var mock = new Mock<ICustomerService<Customer, long>>();
            mock.Setup(service => service.Get(1)).Returns(new Customer { Id = 1, FirstName = "Test1", Surname = "Test1", Email = "Test1@Test.com", Password = "Test123" });
            var controller = new CustomersController(mock.Object);

            // Act
            var OkResult = controller.Get(1).Result as OkObjectResult;
            var customer = Assert.IsType<Customer>(OkResult.Value);

            // Assert
            Assert.Equal(1, customer.Id);
        }
        #endregion

        #region Create/Add Tests
        [Fact]
        public void CustomersController_Add_InvalidObjectPassed_ReturnsBadResult()
        {
            // Arrange
            var customer = new Customer() { Id = 1, Surname = "Test1", Email = "Test1@Test.com", Password = "Test123" };
            var mock = new Mock<ICustomerService<Customer, long>>();
            mock.Setup(service => service.Add(new Customer { Id = 1 })).Returns(1);
            var controller = new CustomersController(mock.Object);
            controller.ModelState.AddModelError("FirstName", "Required");

            // Act
            var badResponse = controller.Post(customer);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void CustomersController_Add_InvalidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var customer = new Customer() { Id = 1, FirstName = "Test1", Surname = "Test1", Email = "Test1@Test.com", Password = "Test123" };
            var mock = new Mock<ICustomerService<Customer, long>>();
            mock.Setup(service => service.Add(customer)).Returns(1);
            var controller = new CustomersController(mock.Object);

            // Act
            var createdResponse = controller.Post(customer);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void CustomersController_Add_InvalidObjectPassed_ReturnsResponseCreatedItem()
        {
            // Arrange
            var customer = new Customer() { Id = 1, FirstName = "Test1", Surname = "Test1", Email = "Test1@Test.com", Password = "Test123" };
            var mock = new Mock<ICustomerService<Customer, long>>();
            mock.Setup(service => service.Add(customer)).Returns(1);
            var controller = new CustomersController(mock.Object);

            // Act
            var createdResponse = controller.Post(customer) as CreatedAtActionResult;
            var customerItem = createdResponse.Value as Customer;

            // Assert
            Assert.IsType<Customer>(customerItem);
            Assert.Equal("Test1", customerItem.FirstName);
        }
        #endregion

        #region Edit/Update Tests
        [Fact]
        public void CustomersController_Edit_InvalidObjectPassed_ReturnsBadResult()
        {
            // Arrange
            var customer = new Customer() { Id = 1, Surname = "Test1", Email = "Test1@Test.com", Password = "Test123" };
            var mock = new Mock<ICustomerService<Customer, long>>();
            mock.Setup(service => service.Update(1, new Customer { Id = 1 })).Returns(1);
            var controller = new CustomersController(mock.Object);
            controller.ModelState.AddModelError("FirstName", "Required");

            // Act
            var badResponse = controller.Put(customer);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void CustomersController_Edit_InvalidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var customer = new Customer() { Id = 1, FirstName = "Test1", Surname = "Test1", Email = "Test1@Test.com", Password = "Test123" };
            var mock = new Mock<ICustomerService<Customer, long>>();
            mock.Setup(service => service.Update(1, customer)).Returns(1);
            var controller = new CustomersController(mock.Object);

            // Act
            var createdResponse = controller.Put(customer);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }

        [Fact]
        public void CustomersController_Edit_InvalidObjectPassed_ReturnsResponseCreatedItem()
        {
            // Arrange
            var customer = new Customer() { Id = 1, FirstName = "Test1", Surname = "Test1", Email = "Test1@Test.com", Password = "Test123" };
            var mock = new Mock<ICustomerService<Customer, long>>();
            mock.Setup(service => service.Update(1, customer)).Returns(1);
            var controller = new CustomersController(mock.Object);

            // Act
            var createdResponse = controller.Put(customer) as CreatedAtActionResult;
            var customerItem = createdResponse.Value as Customer;

            // Assert
            Assert.IsType<Customer>(customerItem);
            Assert.Equal("Test1", customerItem.FirstName);
        }
        #endregion

        #region Delete Tests
        [Fact]
        public void CustomersController_Delete_NoExistingIdPassed_ReturnsNotFoundResult()
        {
            // Arrange
            var customer = new Customer() { Id = 1, FirstName = "Test1", Surname = "Test1", Email = "Test1@Test.com", Password = "Test123" };
            var mock = new Mock<ICustomerService<Customer, long>>();
            mock.Setup(service => service.Get(1)).Returns(customer);
            mock.Setup(service => service.Delete(1)).Returns(1);
            var controller = new CustomersController(mock.Object);

            // Act
            var badGetResponse = controller.Get(2);
            var badDeleteResponse = controller.Delete(2);

            // Assert
            Assert.IsType<NotFoundResult>(badGetResponse.Result);
            Assert.IsType<NotFoundResult>(badDeleteResponse.Result);
        }

        [Fact]
        public void CustomersController_Delete_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var customer = new Customer() { Id = 1, FirstName = "Test1", Surname = "Test1", Email = "Test1@Test.com", Password = "Test123" };
            var mock = new Mock<ICustomerService<Customer, long>>();
            mock.Setup(service => service.Get(1)).Returns(customer);
            mock.Setup(service => service.Delete(1)).Returns(1);
            var controller = new CustomersController(mock.Object);

            // Act
            var OkResponse = controller.Delete(1);

            // Assert
            Assert.IsType<OkObjectResult>(OkResponse.Result);
        }

        [Fact]
        public void CustomersController_Delete_ExistingIdPassed_RemovesItem()
        {
            // Arrange
            var customerList = new List<Customer>() {
                new Customer { Id = 2, FirstName = "Test2", Surname = "Test2", Email = "Test2@Test.com", Password = "Test345" },
                new Customer { Id = 3, FirstName = "Test3", Surname = "Test3", Email = "Test3@Test.com", Password = "Test567"}};
            var mock = new Mock<ICustomerService<Customer, long>>();
            mock.Setup(service => service.GetAll()).Returns(customerList);
            mock.Setup(service => service.Delete(1)).Returns(1);
            var controller = new CustomersController(mock.Object);

            // Act
            var OkResponse = controller.Delete(1);

            // Assert
            Assert.Equal(2, mock.Object.GetAll().Count());
        }
        #endregion
    }
}
