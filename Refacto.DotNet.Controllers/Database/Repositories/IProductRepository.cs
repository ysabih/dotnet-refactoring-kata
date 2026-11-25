using Refacto.DotNet.Controllers.Entities;

namespace Refacto.DotNet.Controllers.Database.Repositories
{
    public interface IProductRepository
    {
        void SetProductLeadTime(Product p, int leadTime);
        void DecrementPorductAvailability(Product p);
        void SetProductUnavailable(Product p);
    }
}
