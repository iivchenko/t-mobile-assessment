using System;
using System.Collections.Generic;
using Stroopwafels.Application.Domain;
using System.ComponentModel.DataAnnotations;

namespace Stroopwafels.Web.Models
{
    public class NewOrderViewModel
    {
        public string CustomerName { get; set; }

        [DataType(DataType.Date)]
        public DateTime WishDate { get; set; }

        public IList<NewOrderLineViewModel> OrderLines { get; set; }

        public NewOrderViewModel()
        {
            OrderLines = new List<NewOrderLineViewModel>
            {
                new NewOrderLineViewModel(0, StroopwafelType.Gewoon),
                new NewOrderLineViewModel(0, StroopwafelType.Suikervrij),
                new NewOrderLineViewModel(0, StroopwafelType.Super)
            };
        }
    }
}