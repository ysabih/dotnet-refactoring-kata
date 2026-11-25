namespace Refacto.DotNet.Controllers.Dtos.Product
{
    public class ProcessOrderResponse { 
        public required long Id { get; init; }
        public List<ProcessProductResponse>? ProductResponses { get; init; }
    }
}
