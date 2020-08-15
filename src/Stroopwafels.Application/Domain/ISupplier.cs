namespace Stroopwafels.Application.Domain
{
    public interface ISupplier
    {
        decimal GetShippingCost(Quote order);

        string Name { get; }
    }
}