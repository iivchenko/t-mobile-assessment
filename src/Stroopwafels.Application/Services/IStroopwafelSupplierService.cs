using Stroopwafels.Application.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stroopwafels.Application.Services
{
    public interface IStroopwafelSupplierService
    {
        ISupplier Supplier { get; }

        bool IsAvailable { get; }

        Task<Quote> GetQuote(IList<KeyValuePair<StroopwafelType, int>> orderDetails);

        Task Order(IList<KeyValuePair<StroopwafelType, int>> quote);
    }
}
