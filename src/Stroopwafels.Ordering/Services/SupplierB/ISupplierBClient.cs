using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stroopwafels.Ordering.Services.SupplierB
{
    public enum SupplierBStroopwafelType
    {
        Gewoon,
        Suikervrij,
        Super,
        Regular,
        SugarFree
    }

    public enum SupplierBBrand
    {
        Stroopie,
        Cuddlies
    }

    public sealed class SupplierBStroopwafel
    {
        [JsonProperty(PropertyName = "Type")]
        public SupplierBStroopwafelType Type { get; set; }

        [JsonProperty(PropertyName = "Merk")]
        public SupplierBBrand Brand { get; set; }

        [JsonProperty(PropertyName = "Prijs")]
        public decimal Price { get; set; }
    }

    public class SupplierBOrderLine
    {
        public int Amount { get; set; }

        public SupplierBOrderProduct Product { get; set; }
    }

    public class SupplierBOrderProduct
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public SupplierBStroopwafelType Type { get; set; }

        [JsonProperty(PropertyName = "Merk")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SupplierBBrand Brand { get; set; }
    }

    public sealed class SupplierBOrder
    {
        public IEnumerable<SupplierBOrderLine> ProductsAndAmounts { get; set; }
    }

    public interface ISupplierBClient
    {
        [Get("/api/Products")]
        Task<IEnumerable<SupplierBStroopwafel>> GetStroopwafels();

        [Post("/api/Order")]
        Task Order([Body] SupplierBOrder order);
    }
}
