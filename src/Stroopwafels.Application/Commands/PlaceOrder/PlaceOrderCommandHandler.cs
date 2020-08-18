using MediatR;
using Stroopwafels.Application.Domain;
using Stroopwafels.Application.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stroopwafels.Application.Commands.PlaceOrder
{
    public sealed class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, Unit>
    {
        private readonly IEnumerable<IStroopwafelSupplierService> _services;

        public PlaceOrderCommandHandler(IEnumerable<IStroopwafelSupplierService> services)
        {
            _services = services;
        }

        public async Task<Unit> Handle(PlaceOrderCommand command, CancellationToken cancellationToken)
        {
            foreach(var group in command.Items.GroupBy(x => x.Supplier))
            {
                var supplier = _services.First(x => x.GetName().GetAwaiter().GetResult() == group.Key);

                var order = new Order
                {
                    ProductsAndAmounts = group.Select(x => new OrderLine(x.Amount, new OrderProduct(x.Type)))
                };

                await supplier.MakeOrder(order);
            }

            return Unit.Value;
        }
    }
}
