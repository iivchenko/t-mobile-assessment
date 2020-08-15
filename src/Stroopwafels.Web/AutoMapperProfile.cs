using AutoMapper;
using Stroopwafels.Application.Domain;
using Stroopwafels.Infrastructure.Services.SupplierA;
using Stroopwafels.Infrastructure.Services.SupplierB;
using Stroopwafels.Infrastructure.Services.SupplierC;

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
        }
    }
}
