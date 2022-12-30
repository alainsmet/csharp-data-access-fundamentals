using Microsoft.AspNetCore.Mvc;
using Moq;
using WarehouseManagementSystem.Infrastructure;
using WarehouseManagementSystem.Web.Controllers;
using WarehouseManagementSystem.Web.Models;

namespace WarehouseManagementSystem.Web.Tests
{
    [TestClass]
    public class OrderControllerTests
    {
        [TestMethod]
        public void CanCreateOrderWithCorrectModel()
        {
            // Arrange
            var orderRepository = new Mock<IRepository<Order>>();
            var itemRepository = new Mock<IRepository<Item>>();
            var shippingProviderRepository = new Mock<IRepository<ShippingProvider>>();

            shippingProviderRepository.Setup(repository => repository.All()).Returns(new[] { new ShippingProvider() }); // Returns a fake ShippingProvider when All() is called

            var orderController = new OrderController(
                orderRepository.Object,
                shippingProviderRepository.Object,
                itemRepository.Object
                );

            var createOrderModel = new CreateOrderModel
            {
                Customer = new()
                {
                    Name = "Alain Smet",
                    Address = "Apple Street, 23",
                    PostalCode= "12345",
                    Country = "Madeira",
                    PhoneNumber = "0987654321"
                },
                LineItems = new[]
                {
                    new LineItemModel
                    {
                        ItemId = Guid.NewGuid(),
                        Quantity = 100
                    }
                }
            };

            // Act
            orderController.Create(createOrderModel);

            // Assert
            orderRepository.Verify(CustomerRepository => CustomerRepository.Add(It.IsAny<Order>()), Times.AtMostOnce());
        }
    }
}