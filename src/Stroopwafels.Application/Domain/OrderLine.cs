namespace Stroopwafels.Application.Domain
{
    public sealed class OrderLine
    {
        public int Amount { get; set; }
        
        public OrderProduct Product { get; set; }
    }
}