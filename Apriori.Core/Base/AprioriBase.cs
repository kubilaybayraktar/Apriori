using System.Collections.Generic;
using System.Linq;

namespace Apriori.Core
{
    /// <summary>
    /// Base Class for common usage
    /// </summary>
    public abstract class AprioriBase
    {
        /// <summary>
        /// Get Combinations of Group
        /// </summary>
        /// <typeparam name="T">Type Of Object</typeparam>
        /// <param name="items">List of Items</param>
        /// <param name="count">Total Count</param>
        /// <returns></returns>
        internal IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> items, int count)
        {
            int i = 0;
            foreach (var item in items)
            {
                if (count == 1)
                    yield return new T[] { item };
                else
                {
                    foreach (var result in GetPermutations(items.Skip(i + 1), count - 1))
                        yield return new T[] { item }.Concat(result);
                }

                ++i;
            }
        }
    }
}
