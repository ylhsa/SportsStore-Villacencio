using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Can_Use_Repository()
        {
            // Arrange
            var mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" }
            }).AsQueryable());

            var controller = new HomeController(mock.Object);

            // Act
            var result = (controller.Index(null, 1) as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            // Assert
            Assert.NotNull(result); // Ensure the result is not null
            var prodArray = result.ToArray();
            Assert.Equal(2, prodArray.Length); // Check the number of products
            Assert.Equal("P1", prodArray[0].Name); // Validate the name of the first product
            Assert.Equal("P2", prodArray[1].Name); // Validate the name of the second product
        }
    }
}
