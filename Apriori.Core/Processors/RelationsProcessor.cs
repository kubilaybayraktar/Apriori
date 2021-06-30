using System;
using System.Collections.Generic;
using System.Linq;

namespace Apriori.Core
{
    public class RelationsProcessor : AprioriBase, IRelationsProcessor
    {
        /// <summary>
        /// Get Relations
        /// </summary>
        /// <param name="lists">List Of Items</param>
        /// <param name="groupItemsCount">Count</param>
        /// <returns>List of Relations</returns>
        public IEnumerable<IEnumerable<IItem>> GetRelations(IEnumerable<IItem> lists, int groupItemsCount)
        {
            if (lists == null || !lists.ToList().Any())
                throw new Exception("Please supply a list!");

            if (groupItemsCount < 1)
                throw new Exception("Parameter must be greater than 1: " + nameof(groupItemsCount));

            return GetPermutations(lists, groupItemsCount);
        }
    }
}