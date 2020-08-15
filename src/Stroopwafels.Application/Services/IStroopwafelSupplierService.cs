using Stroopwafels.Application.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stroopwafels.Application.Services
{
    public interface IStroopwafelSupplierService
    {
        ISupplier Supplier { get; }

        bool IsAvailable { get; }

        Task<IEnumerable<Stroopwafel>> QueryStroopwafels();

        Task Order(IList<KeyValuePair<StroopwafelType, int>> quote);
    }
}
