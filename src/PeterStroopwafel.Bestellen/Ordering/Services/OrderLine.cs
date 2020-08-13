namespace Ordering.Services
{
    public class OrderLine
    {
        public int Amount { get; }
        
        public OrderProduct Product { get; }

        public OrderLine(int amount, OrderProduct product)
        {
            this.Amount = amount;
            this.Product = product;
        }
    }
}