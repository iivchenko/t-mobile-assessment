using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stroopwafels.Infrastructure.Services.SupplierA
{
    public enum SupplierAStroopwafelType
    {
        Gewoon,
        Suikervrij,
        Super,
        Regular,
        SugarFree
    }

    public enum SupplierABrand
    {
        Stroopie,
        Cuddlies
    }

    public sealed class SupplierAStroopwafel
    {
        [JsonProperty(PropertyName = "Type")]
        public SupplierAStroopwafelType Type { get; set; }

        [JsonProperty(PropertyName = "Merk")]
        public SupplierABrand Brand { get; set; }

        [JsonProperty(PropertyName = "Prijs")]
        public decimal Price { get; set; }
    }

    public class SupplierAOrderLine
    {
        public int Amount { get; set; }

        public SupplierAOrderProduct Product { get; set; }
    }

    public class SupplierAOrderProduct
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public SupplierAStroopwafelType Type { get; set; }

        [JsonProperty(PropertyName = "Merk")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SupplierABrand Brand { get; set; }
    }

    public sealed class SupplierAOrder
    {
        public IEnumerable<SupplierAOrderLine> ProductsAndAmounts { get; set; }
    }

    public interface ISupplierAClient
    {
        [Get("/api/Products")]
        Task<IEnumerable<SupplierAStroopwafel>> GetStroopwafels();

        [Post("/api/Order")]
        Task Order([Body] SupplierAOrder order);
    }
}
