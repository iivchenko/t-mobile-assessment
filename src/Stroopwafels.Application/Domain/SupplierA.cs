namespace Stroopwafels.Application.Domain
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
