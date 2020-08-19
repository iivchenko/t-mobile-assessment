using System.ComponentModel.DataAnnotations;
using Stroopwafels.Application.Domain;

namespace Stroopwafels.Web.Models
{
    public sealed class NewOrderLineViewModel
    {
        public NewOrderLineViewModel()
        { 
        }

        public NewOrderLineViewModel(int amount, StroopwafelType type)
        {
            Amount = amount;
            Type = type;
        }

        [Required]
        public int Amount { get; set; }

        [Required]
        public StroopwafelType Type { get; set; }
    }
}