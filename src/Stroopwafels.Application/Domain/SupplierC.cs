using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stroopwafels.Application.Domain
{
    public class SupplierC : ISupplier
    {
        private const int ShippingCostPercentage = 5;
        
        public decimal GetShippingCost(Quote order)
        {
            return order.TotalWithoutShippingCost / 100 * ShippingCostPercentage;
        }

        public string Name => "Leverancier C";
    }
}
