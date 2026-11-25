using Microsoft.EntityFrameworkCore;
using Refacto.DotNet.Controllers.Database.Context;
using Refacto.DotNet.Controllers.Dtos.Product;

namespace Refacto.DotNet.Controllers.Services.Impl
{
    public class OrderService
    {
        private readonly IProductService productService;
        private readonly AppDbContext appDbContext;

        public OrderService(AppDbContext appDbContext, IProductService productService)
        {
            this.appDbContext = appDbContext;
            this.productService = productService;
        }

        public ProcessOrderResponse ProcessOrder(long orderId)
        {
            Entities.Order? order = appDbContext.Orders
                            .Include(o => o.Items)
                            .SingleOrDefault(o => o.Id == orderId);
            Console.WriteLine(order);

            ICollection<Entities.Product>? products = order.Items;

            foreach (Entities.Product p in products)
            {
                if (p.Type == "NORMAL")
                {
                    HandleNormalProduct(p);
                }
                else if (p.Type == "SEASONAL")
                {
                    productService.HandleSeasonalProduct(p);
                }
                else if (p.Type == "EXPIRABLE")
                {
                    productService.HandleExpirableProduct(p);
                }
            }

            return new ProcessOrderResponse { Id = order.Id };
        }

        private void HandleNormalProduct(Entities.Product p)
        {
            if (p.Available > 0)
            {
                p.Available -= 1;
                appDbContext.Entry(p).State = EntityState.Modified;
                _ = appDbContext.SaveChanges();

            }
            else
            {
                int leadTime = p.LeadTime;
                if (leadTime > 0)
                {
                    productService.NotifyDelay(leadTime, p);
                }
            }
        }
    }
}
