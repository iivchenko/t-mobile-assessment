using System;
using System.ComponentModel.DataAnnotations;

namespace Stroopwafels.Web.Models
{
    public sealed class OrderViewModel
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        public DateTime WishDate { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public OrderLineViewModel[] OrderLines { get; set; }
    }
}