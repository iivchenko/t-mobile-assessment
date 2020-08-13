using System.ComponentModel.DataAnnotations;
using Stroopwafels.Ordering;

namespace Stroopwafels.Web.Models
{
    public class OrderRow
    {
        [Required]
        public int Amount { get; set; }

        public StroopwafelType Type { get; set; }

        public string DisplayName => this.Type.ToString();

        public OrderRow()
        {
        }

        public OrderRow(int amount, StroopwafelType type)
        {
            Amount = amount;
            Type = type;
        }
    }
}