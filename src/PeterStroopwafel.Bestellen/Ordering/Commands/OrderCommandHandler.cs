using System;
using System.Collections.Generic;
using System.Linq;
using Ordering.Queries;

namespace Ordering.Commands
{
    public class OrderCommandHandler
    {
        private readonly IEnumerable<IStroopwafelSupplierService> _stroopwafelSupplierServices;

        public OrderCommandHandler(IEnumerable<IStroopwafelSupplierService> stroopwafelSupplierServices)
        {
            _stroopwafelSupplierServices = stroopwafelSupplierServices;
        }

        public void Handle(OrderCommand command)
        {
            var stroopwafelSupplierService = this._stroopwafelSupplierServices.Single(
                service =>
                    service.Supplier.Name.Equals(command.Supplier, StringComparison.InvariantCultureIgnoreCase));

            stroopwafelSupplierService.Order(command.OrderLines);
        }
    }
}
