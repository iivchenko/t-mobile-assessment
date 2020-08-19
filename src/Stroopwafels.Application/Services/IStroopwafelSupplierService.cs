using System;
using Stroopwafels.Application.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stroopwafels.Application.Services
{
    public interface IStroopwafelSupplierService
    {
        string Name { get; }

        Task<TimeSpan> GetDeliveryPeriod();

        Task<decimal> CalculateShipingCost(decimal totalPrice);

        Task<IEnumerable<Stroopwafel>> QueryStroopwafels();

        Task MakeOrder(Order order);
    }
}
