﻿using System.Collections.Generic;

namespace Stroopwafels.Ordering
{
    public interface IStroopwafelSupplierService
    {
        ISupplier Supplier { get; }

        bool IsAvailable { get; }

        Quote GetQuote(IList<KeyValuePair<StroopwafelType, int>> orderDetails);

        void Order(IList<KeyValuePair<StroopwafelType, int>> quote);
    }
}