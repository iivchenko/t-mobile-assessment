using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Stroopwafels.Application.Domain
{
    public class OrderProduct
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public StroopwafelType Type { get; }

        [JsonProperty(PropertyName = "Merk")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Brand Brand { get; }

        public OrderProduct(StroopwafelType type)
        {
            this.Type = type;
            this.Brand = Brand.Stroopie;
        }
    }
}