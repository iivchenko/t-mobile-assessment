using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ordering
{
    public class Quote
    {
        public decimal TotalPrice => this.TotalWithoutShippingCost + this.Supplier.GetShippingCost(this);

        public string TotalPricePresentation => this.TotalPrice.ToString("C");

        public decimal TotalWithoutShippingCost
        {
            get { return this.OrderLines.Sum(o => o.Price); }
        }

        public IList<QuoteLine> OrderLines { get; }
        
        public ISupplier Supplier { get; }

        public Quote(IList<QuoteLine> orderLines, ISupplier supplier)
        {
            this.OrderLines = orderLines;
            this.Supplier = supplier;
        }
    }
}