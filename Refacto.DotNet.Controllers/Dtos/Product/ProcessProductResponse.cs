namespace Refacto.DotNet.Controllers.Dtos.Product
{
    public class ProcessProductResponse
    {
        public long Id { get; init; }

        public ProductOrderState State { get; init; }
    }

    public class DelayedProductResponse
    {
        public TimeSpan Delay { get; init; }
    }

    public enum ProductOrderState {
        Success,
        Unavailable,
        Delayed,
    }
}
