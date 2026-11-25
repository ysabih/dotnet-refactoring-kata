using Refacto.DotNet.Controllers.Database.Context;
using Refacto.DotNet.Controllers.Entities;

namespace Refacto.DotNet.Controllers.Database.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext appDbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            appDbContext = dbContext;
        }

        public void DecrementPorductAvailability(Product product)
        {
            product.Available = product.Available - 1;
            appDbContext.SaveChanges();
        }

        public void SetProductLeadTime(Product p, int leadTime)
        {
            p.LeadTime = leadTime;
            appDbContext.SaveChanges();
        }

        public void SetProductUnavailable(Product p)
        {
            p.Available = 0;
            appDbContext.SaveChanges();
        }
    }
}
