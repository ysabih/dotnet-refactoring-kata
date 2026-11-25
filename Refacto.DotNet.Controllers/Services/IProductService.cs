using Refacto.DotNet.Controllers.Entities;

namespace Refacto.DotNet.Controllers.Services
{
    public interface IProductService
    {
        void HandleExpirableProduct(Product p);
        void HandleSeasonalProduct(Product p);
        void NotifyDelay(int leadTime, Product p);
    }
}