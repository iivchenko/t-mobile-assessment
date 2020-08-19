namespace Stroopwafels.Application.Domain
{
    public sealed class OrderProduct
    {
        public StroopwafelType Type { get; set; }

        public Brand Brand { get; set; }
    }
}