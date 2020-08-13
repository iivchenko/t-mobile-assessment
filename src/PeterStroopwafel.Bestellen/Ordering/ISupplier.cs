namespace Ordering
{
    public interface ISupplier
    {
        decimal GetShippingCost(Quote order);

        string Name { get; }
    }
}