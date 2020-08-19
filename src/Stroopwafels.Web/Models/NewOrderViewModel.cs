using System;
using Stroopwafels.Application.Domain;
using System.ComponentModel.DataAnnotations;

namespace Stroopwafels.Web.Models
{
    public sealed class NewOrderViewModel
    {
        public NewOrderViewModel()
        {
            OrderLines = new []
            {
                new NewOrderLineViewModel(0, StroopwafelType.Gewoon),
                new NewOrderLineViewModel(0, StroopwafelType.Suikervrij),
                new NewOrderLineViewModel(0, StroopwafelType.Super)
            };
        }

        public string CustomerName { get; set; }

        [DataType(DataType.Date)]
        public DateTime WishDate { get; set; }

        public NewOrderLineViewModel[] OrderLines { get; set; }
    }
}