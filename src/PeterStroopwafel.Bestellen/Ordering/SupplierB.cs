using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering
{
    public class SupplierB : ISupplier
    {
        private const decimal ShippingCostLimit = 50m;

        private const decimal ShippingCostAboveLimit = 0m;

        private const decimal ShippingCostUnderLimit = 5m;

        public decimal GetShippingCost(Quote order)
        {
            return order.TotalWithoutShippingCost > ShippingCostLimit ? ShippingCostAboveLimit : ShippingCostUnderLimit;
        }

        public string Name => "Leverancier B";
    }
}
