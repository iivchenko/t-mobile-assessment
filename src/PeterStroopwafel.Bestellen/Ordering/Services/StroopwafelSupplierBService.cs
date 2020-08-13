using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Services
{
    public class StroopwafelSupplierBService : StroopwafelSupplierServiceBase, IStroopwafelSupplierService
    {
        private static readonly Uri ProductsUri = new Uri("http://stroopwafelb.azurewebsites.net/api/Products");

        private static readonly Uri OrderUri = new Uri("http://stroopwafelb.azurewebsites.net/api/Order");

        public ISupplier Supplier => new SupplierB();

        private static readonly IList<DateTime> PublicHolidays = new List<DateTime>
        {
            new DateTime(2016, 1, 1),
            new DateTime(2016, 12, 25),
            new DateTime(2016, 12, 26)
        }; 

        public bool IsAvailable => this.GetAvailability();
        
        public StroopwafelSupplierBService(IHttpClientWrapper httpClientWrapper) : base(httpClientWrapper)
        {
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

        public Quote GetQuote(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            if (!this.IsAvailable)
            {
                return null;
            }

            var result = base.ExecuteGet(ProductsUri);
            var stroopwafels = result.ToObject<IList<Stroopwafel>>();

            var builder = new QuoteBuilder();
            return builder.CreateOrder(orderDetails, stroopwafels, new SupplierB());
        }

        public void Order(IList<KeyValuePair<StroopwafelType, int>> quoteLines)
        {
            var builder = new OrderBuilder();
            Order order = builder.CreateOrder(quoteLines);
            base.ExecutePost(OrderUri, order);
        }
    }
}