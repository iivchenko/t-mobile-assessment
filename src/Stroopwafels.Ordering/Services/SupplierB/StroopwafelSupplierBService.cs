using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stroopwafels.Ordering.Services.SupplierB
{
    public sealed class StroopwafelSupplierBService : IStroopwafelSupplierService
    {
        private readonly ISupplierBClient _client;
        private readonly IMapper _mapper;

        public ISupplier Supplier => new Ordering.SupplierB();

        private static readonly IList<DateTime> PublicHolidays = new List<DateTime>
        {
            new DateTime(2016, 1, 1),
            new DateTime(2016, 12, 25),
            new DateTime(2016, 12, 26)
        }; 

        public bool IsAvailable => this.GetAvailability();

        public StroopwafelSupplierBService(ISupplierBClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        private bool GetAvailability()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            var isHoliday = PublicHolidays.Any(h => h == DateTime.Now.Date);
            return !isHoliday;
        }

        public async Task<Quote> GetQuote(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            if (!this.IsAvailable)
            {
                return null;
            }

            var supplierStroopwafels = await _client.GetStroopwafels();
            var stroopwafels = _mapper.Map<IEnumerable<Stroopwafel>>(supplierStroopwafels);

            var builder = new QuoteBuilder();
            var order = builder.CreateOrder(orderDetails, stroopwafels.ToList(), Supplier);

            return order;
        }

        public async Task Order(IList<KeyValuePair<StroopwafelType, int>> quoteLines)
        {
            var builder = new OrderBuilder();
            var order = builder.CreateOrder(quoteLines);
            var supplierOrder = _mapper.Map<SupplierBOrder>(order);

            await _client.Order(supplierOrder);
        }
    }
}