using MediatR;
using Stroopwafels.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stroopwafels.Application.Commands
{
    public sealed class OrderCommandHandler : IRequestHandler<OrderCommand, Unit>
    {
        private readonly IEnumerable<IStroopwafelSupplierService> _stroopwafelSupplierServices;

        public OrderCommandHandler(IEnumerable<IStroopwafelSupplierService> stroopwafelSupplierServices)
        {
            _stroopwafelSupplierServices = stroopwafelSupplierServices;
        }

        public async Task<Unit> Handle(OrderCommand command, CancellationToken cancellationToken)
        {
            var stroopwafelSupplierService = 
                _stroopwafelSupplierServices
                    .Single(service => service.Supplier.Name.Equals(command.Supplier, StringComparison.InvariantCultureIgnoreCase));

            var builder = new Domain.OrderBuilder();
            var order = builder.CreateOrder(command.OrderLines);

            await stroopwafelSupplierService.MakeOrder(order);

            return Unit.Value;
        }
    }
}
