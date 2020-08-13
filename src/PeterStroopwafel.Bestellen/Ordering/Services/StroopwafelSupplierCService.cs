using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Services
{
    public class StroopwafelSupplierCService : StroopwafelSupplierServiceBase, IStroopwafelSupplierService
    {
        private static readonly Uri ProductsUri = new Uri("http://stroopwafelc.azurewebsites.net/api/Products");

        private static readonly Uri OrderUri = new Uri("http://stroopwafelc.azurewebsites.net/api/Order");

        public ISupplier Supplier => new SupplierC();

        public bool IsAvailable => true;

        public StroopwafelSupplierCService(IHttpClientWrapper httpClientWrapper) : base(httpClientWrapper)
        {
        }

        public Quote GetQuote(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            var result = base.ExecuteGet(ProductsUri);
            var stroopwafels = result.ToObject<IList<Stroopwafel>>();

            var builder = new QuoteBuilder();

            return builder.CreateOrder(orderDetails, stroopwafels, new SupplierC());
        }

        public void Order(IList<KeyValuePair<StroopwafelType, int>> quoteLines)
        {
            var builder = new OrderBuilder();
            Order order = builder.CreateOrder(quoteLines);
            base.ExecutePost(OrderUri, order);
        }
    }
}