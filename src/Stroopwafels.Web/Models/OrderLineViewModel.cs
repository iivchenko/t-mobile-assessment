using Stroopwafels.Application.Domain;
using System.ComponentModel.DataAnnotations;

namespace Stroopwafels.Web.Models
{
    public class OrderLineViewModel
    {
        [Required]
        public int Amount { get; set; }

        [Required]
        [Display(Name = "Stroopwafel")]
        public StroopwafelType Type { get; set; }

        [Required]
        public string Supplier { get; set; }
    }
}