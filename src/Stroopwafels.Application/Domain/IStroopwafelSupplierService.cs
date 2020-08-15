using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stroopwafels.Application.Domain
{
    public interface IStroopwafelSupplierService
    {
        ISupplier Supplier { get; }

        bool IsAvailable { get; }

        Task<Quote> GetQuote(IList<KeyValuePair<StroopwafelType, int>> orderDetails);

        Task Order(IList<KeyValuePair<StroopwafelType, int>> quote);
    }
}
