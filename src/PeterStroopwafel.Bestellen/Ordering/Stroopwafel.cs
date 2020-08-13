using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ordering
{
    public class Stroopwafel
    {
        [JsonProperty(PropertyName = "Type")]
        public StroopwafelType Type { get; set; }

        [JsonProperty(PropertyName = "Merk")]
        public Brand Brand { get; set; }

        [JsonProperty(PropertyName = "Prijs")]
        public decimal Price { get; set; }

        [JsonConstructor]
        public Stroopwafel(StroopwafelType type, Brand brand, decimal price)
        {
            this.Type = type;
            this.Brand = brand;
            this.Price = price;
        }

        public Stroopwafel(StroopwafelType type)
        {
            this.Type = type;
        }
    }
}
