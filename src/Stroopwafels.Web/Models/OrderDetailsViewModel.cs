using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stroopwafels.Application.Domain;

namespace Stroopwafels.Web.Models
{
    public class OrderDetailsViewModel
    {
        public IList<OrderRow> OrderRows { get; set; }

        public OrderDetailsViewModel()
        {
            this.OrderRows = new List<OrderRow>
            {
                new OrderRow(0, StroopwafelType.Gewoon),
                new OrderRow(0, StroopwafelType.Suikervrij),
                new OrderRow(0, StroopwafelType.Super)
            };
        }
    }
}