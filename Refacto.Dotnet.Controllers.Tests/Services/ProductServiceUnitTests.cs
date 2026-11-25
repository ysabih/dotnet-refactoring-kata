using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Refacto.DotNet.Controllers.Database.Context;
using Refacto.DotNet.Controllers.Database.Repositories;
using Refacto.DotNet.Controllers.Entities;
using Refacto.DotNet.Controllers.Services;
using Refacto.DotNet.Controllers.Services.Impl;

namespace Refacto.DotNet.Controllers.Tests.Services
{
    public class ProductServiceUnitTests
    {
        private readonly Mock<INotificationService> mockNotificationService;
        private readonly Mock<IProductRepository> mockProductRepository;
        private readonly ProductService productService;

        public ProductServiceUnitTests()
        {
            mockNotificationService = new Mock<INotificationService>();
            mockProductRepository = new Mock<IProductRepository>();

            productService = new ProductService(mockNotificationService.Object, mockProductRepository.Object);
        }

        [Fact]
        public void NotifyDelay_DelayNotificationIsSentWithCorrectLeadTimeAndLeatTimeIsSetOnProduct()
        {
            // GIVEN
            Product product = new()
            {
                LeadTime = 15,
                Available = 0,
                Type = "NORMAL",
                Name = "RJ45 Cable"
            };

            // WHEN
            productService.NotifyDelay(product.LeadTime, product);

            // THEN
            Assert.Equal(0, product.Available);
            Assert.Equal(15, product.LeadTime);
            mockProductRepository.Verify(repo => repo.SetProductLeadTime(product, product.LeadTime), Times.Once());
            mockNotificationService.Verify(service => service.SendDelayNotification(product.LeadTime, product.Name), Times.Once());
        }

    }
}
