using MediatR;
using Stroopwafels.Ordering;
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

        public Task<Unit> Handle(OrderCommand command, CancellationToken cancellationToken)
        {
            // TODO: make async
            var stroopwafelSupplierService = this._stroopwafelSupplierServices.Single(
                service =>
                    service.Supplier.Name.Equals(command.Supplier, StringComparison.InvariantCultureIgnoreCase));

            stroopwafelSupplierService.Order(command.OrderLines);

            return Unit.Task;
        }
    }
}
