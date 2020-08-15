using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stroopwafels.Ordering.Services
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

        public Task<Quote> GetQuote(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            var result = base.ExecuteGet(ProductsUri);
            var stroopwafels = result.ToObject<IList<Stroopwafel>>();

            var builder = new QuoteBuilder();

            var order = builder.CreateOrder(orderDetails, stroopwafels, new SupplierC());

            return Task.FromResult(order);
        }

        public Task Order(IList<KeyValuePair<StroopwafelType, int>> quoteLines)
        {
            var builder = new OrderBuilder();
            Order order = builder.CreateOrder(quoteLines);
            base.ExecutePost(OrderUri, order);

            return Task.CompletedTask;
        }
    }
}