namespace Stroopwafels.Application.Domain
{
    public sealed class Stroopwafel
    {
        public StroopwafelType Type { get; set; }

        public Brand Brand { get; set; }

        public decimal Price { get; set; }
    }
}
