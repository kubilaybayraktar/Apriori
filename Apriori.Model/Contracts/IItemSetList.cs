using System.Collections.Generic;

namespace Apriori.Core
{
    public interface IItemSetList
    {
        ItemSetList CalculateCount(IEnumerable<IEnumerable<IItem>> relations, int minSup);
    }
}
