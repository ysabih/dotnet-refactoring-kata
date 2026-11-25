using Refacto.DotNet.Controllers.Database.Context;
using Refacto.DotNet.Controllers.Database.Repositories;
using Refacto.DotNet.Controllers.Entities;

namespace Refacto.DotNet.Controllers.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly INotificationService notificationService;

        private readonly IProductRepository productRepository;

        public ProductService(INotificationService ns, IProductRepository productRepository)
        {
            notificationService = ns;
            this.productRepository = productRepository;
        }

        public void NotifyDelay(int leadTime, Product p)
        {
            productRepository.SetProductLeadTime(p, leadTime);
            notificationService.SendDelayNotification(leadTime, p.Name);
        }

        public void HandleSeasonalProduct(Product p)
        {
            if (DateTime.Now.AddDays(p.LeadTime) > p.SeasonEndDate)
            {
                notificationService.SendOutOfStockNotification(p.Name);

                productRepository.SetProductUnavailable(p);
            }
            else if (p.SeasonStartDate > DateTime.Now)
            {
                notificationService.SendOutOfStockNotification(p.Name);
            }
            else
            {
                NotifyDelay(p.LeadTime, p);
            }
        }



        public void HandleExpiredProduct(Product p)
        {
            if (p.Available > 0 && p.ExpiryDate > DateTime.Now)
            {
                productRepository.DecrementPorductAvailability(p);
            }
            else
            {
                notificationService.SendExpirationNotification(p.Name, (DateTime)p.ExpiryDate);

                productRepository.SetProductUnavailable(p);
            }
        }
    }
}
