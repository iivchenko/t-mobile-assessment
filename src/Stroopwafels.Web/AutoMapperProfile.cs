using AutoMapper;
using Stroopwafels.Ordering;
using Stroopwafels.Ordering.Services;
using Stroopwafels.Ordering.Services.SupplierA;

namespace Stroopwafels.Web
{
    public sealed class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SupplierAStroopwafel, Stroopwafel>();
            CreateMap<Order, SupplierAOrder>();
        }
    }
}
