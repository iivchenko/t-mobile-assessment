using System;

namespace Stroopwafels.Web.Models
{
    public class OrderViewModel
    {
        public string CustomerName { get; set; }

        public DateTime WishDate { get; set; }

        public decimal TotalPrice { get; set; }

        public OrderLineViewModel[] OrderLines { get; set; }
    }
}