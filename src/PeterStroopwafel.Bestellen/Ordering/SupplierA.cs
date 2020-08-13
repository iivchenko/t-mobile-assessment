using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering
{
    public class SupplierA : ISupplier
    {
        public decimal GetShippingCost(Quote order)
        {
            return 5m;
        }

        public string Name => "Leverancier A";
    }
}
