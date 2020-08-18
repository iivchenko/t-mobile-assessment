﻿using System;
using MediatR;
using Stroopwafels.Application.Domain;
using System.Collections.Generic;

namespace Stroopwafels.Application.Queries.GetQuotes
{
    public sealed class GetQuotesQuery : IRequest<GetQuotesQueryResponse>
    {
        public IEnumerable<QuotesItem> Items { get; set; }

        public QuotesCustomer Customer { get; set; }

    }

    public sealed class QuotesItem
    {
        public StroopwafelType Type { get; set; }

        public int Amount { get; set; }
    }

    public sealed class QuotesCustomer
    {
        public string Name { get; set; }

        public DateTime WishDate { get; set; }
    }
}