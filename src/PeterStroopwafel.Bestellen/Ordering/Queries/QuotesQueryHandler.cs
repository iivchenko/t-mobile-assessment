using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Queries
{
    public class QuotesQueryHandler
    {
        private readonly IEnumerable<IStroopwafelSupplierService> _stroopwafelSupplierServices;

        public QuotesQueryHandler(IEnumerable<IStroopwafelSupplierService> stroopwafelSupplierServices)
        {
            _stroopwafelSupplierServices = stroopwafelSupplierServices;
        }

        public IList<Quote> Handle(QuotesQuery query)
        {
            return this._stroopwafelSupplierServices.Where(service => service.IsAvailable).Select(service => service.GetQuote(query.OrderLines)).ToList();
        }
    }
}
