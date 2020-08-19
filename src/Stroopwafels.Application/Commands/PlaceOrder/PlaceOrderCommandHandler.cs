using AutoMapper;
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
        private readonly IMapper _mapper;

        public PlaceOrderCommandHandler(IEnumerable<IStroopwafelSupplierService> services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(PlaceOrderCommand command, CancellationToken cancellationToken)
        {
            foreach(var group in command.OrderLines.GroupBy(x => x.Supplier))
            {
                var supplier = _services.First(x => x.GetName().GetAwaiter().GetResult() == group.Key);

                var order = new Order
                {
                    ProductsAndAmounts = _mapper.Map<IEnumerable<OrderLine>>(group)
                };

                await supplier.MakeOrder(order);
            }

            return Unit.Value;
        }
    }
}
