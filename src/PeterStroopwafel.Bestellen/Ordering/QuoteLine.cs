using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Ordering
{
    public class QuoteLine
    {
        public int Amount { get; }

        [JsonProperty(PropertyName = "Product")]
        public Stroopwafel Stroopwafel { get; }

        public decimal Price => this.Amount * this.Stroopwafel.Price;

        public QuoteLine(int amount, Stroopwafel stroopwafel)
        {
            this.Amount = amount;
            this.Stroopwafel = stroopwafel;
        }

    }
}
