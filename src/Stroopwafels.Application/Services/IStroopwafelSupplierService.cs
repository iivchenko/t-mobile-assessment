using Stroopwafels.Application.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stroopwafels.Application.Services
{
    public interface IStroopwafelSupplierService
    {
        bool IsAvailable { get; }

        Task<string> GetName();

        Task<decimal> CalculateShipingCost(decimal totalPrice);

        Task<IEnumerable<Stroopwafel>> QueryStroopwafels();

        Task MakeOrder(Order order);
    }
}
