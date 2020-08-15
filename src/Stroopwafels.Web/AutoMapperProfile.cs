using AutoMapper;
using Stroopwafels.Ordering;
using Stroopwafels.Ordering.Services;
using Stroopwafels.Ordering.Services.SupplierA;
using Stroopwafels.Ordering.Services.SupplierB;

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
        }
    }
}
