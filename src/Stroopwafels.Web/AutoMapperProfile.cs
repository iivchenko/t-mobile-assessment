using AutoMapper;
using Stroopwafels.Application.Commands.PlaceOrder;
using Stroopwafels.Application.Domain;
using Stroopwafels.Application.Queries.GetQuotes;
using Stroopwafels.Infrastructure.Services.SupplierA;
using Stroopwafels.Infrastructure.Services.SupplierB;
using Stroopwafels.Infrastructure.Services.SupplierC;
using Stroopwafels.Web.Models;

namespace Stroopwafels.Web
{
    public sealed class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SupplierAStroopwafel, Stroopwafel>();
            CreateMap<OrderProduct, SupplierAOrderProduct>();
            CreateMap<OrderLine, SupplierAOrderLine>();
            CreateMap<Order, SupplierAOrder>();

            CreateMap<SupplierBStroopwafel, Stroopwafel>();
            CreateMap<OrderProduct, SupplierBOrderProduct>();
            CreateMap<OrderLine, SupplierBOrderLine>();
            CreateMap<Order, SupplierBOrder>();

            CreateMap<SupplierCStroopwafel, Stroopwafel>();
            CreateMap<OrderProduct, SupplierCOrderProduct>();
            CreateMap<OrderLine, SupplierCOrderLine>();
            CreateMap<Order, SupplierCOrder>();

            CreateMap<NewOrderViewModel, GetQuotesQuery>();
            CreateMap<NewOrderLineViewModel, QuotesOrderLine>();

            CreateMap<GetQuotesQueryResponse, OrderViewModel>();
            CreateMap<QuotesQueryItem, OrderLineViewModel>();

            CreateMap<OrderViewModel, PlaceOrderCommand>();
            CreateMap<OrderLineViewModel, PlaceOrderItem>();

            CreateMap<PlaceOrderItem, OrderLine>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => new OrderProduct { Type = src.Type, Brand = Brand.Stroopie }));
        }
    }
}
