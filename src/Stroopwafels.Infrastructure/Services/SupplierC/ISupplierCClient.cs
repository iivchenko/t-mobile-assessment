using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stroopwafels.Infrastructure.Services.SupplierC
{
    public enum SupplierCStroopwafelType
    {
        Gewoon,
        Suikervrij,
        Super,
        Regular,
        SugarFree
    }

    public enum SupplierCBrand
    {
        Stroopie,
        Cuddlies
    }

    public sealed class SupplierCStroopwafel
    {
        [JsonProperty(PropertyName = "Type")]
        public SupplierCStroopwafelType Type { get; set; }

        [JsonProperty(PropertyName = "Merk")]
        public SupplierCBrand Brand { get; set; }

        [JsonProperty(PropertyName = "Prijs")]
        public decimal Price { get; set; }
    }

    public class SupplierCOrderLine
    {
        public int Amount { get; set; }

        public SupplierCOrderProduct Product { get; set; }
    }

    public class SupplierCOrderProduct
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public SupplierCStroopwafelType Type { get; set; }

        [JsonProperty(PropertyName = "Merk")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SupplierCBrand Brand { get; set; }
    }

    public sealed class SupplierCOrder
    {
        public IEnumerable<SupplierCOrderLine> ProductsAndAmounts { get; set; }
    }

    public interface ISupplierCClient
    {
        [Get("/api/Products")]
        Task<IEnumerable<SupplierCStroopwafel>> GetStroopwafels();

        [Post("/api/Order")]
        Task Order([Body] SupplierCOrder order);
    }
}
