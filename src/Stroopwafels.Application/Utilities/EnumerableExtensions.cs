using System.Collections.Generic;
using System.Linq;

namespace Stroopwafels.Application.Utilities
{
    public static class EnumerableExtensions
    {
        public static async IAsyncEnumerable<IAsyncEnumerable<T>> CartesianProduct<T>(this IAsyncEnumerable<IAsyncEnumerable<T>> sequences)
        {
            if (sequences == null)
            {
                yield break;
            }

            IEnumerable<IAsyncEnumerable<T>> emptyProduct = new[] { AsyncEnumerable.Empty<T>() };

            var task = sequences.AggregateAsync(
                emptyProduct.ToAsyncEnumerable(),
                (accumulator, sequence) => accumulator.SelectMany(
                    accseq => sequence,
                    (accseq, item) => accseq.Concat((new[] { item }).ToAsyncEnumerable())));

            await foreach(var combination in await task)
            {
                yield return combination;
            }
        }
    }
}
