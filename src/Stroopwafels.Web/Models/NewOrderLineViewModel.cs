using System.ComponentModel.DataAnnotations;
using Stroopwafels.Application.Domain;

namespace Stroopwafels.Web.Models
{
    public class NewOrderLineViewModel
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
        [Display(Name = "Stroopwafel")]
        public StroopwafelType Type { get; set; }
    }
}