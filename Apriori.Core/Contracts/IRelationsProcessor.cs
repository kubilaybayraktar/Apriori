using System.Collections.Generic;

namespace Apriori.Core
{
    public interface IRelationsProcessor
    {
        IEnumerable<IEnumerable<IItem>> GetRelations(IEnumerable<IItem> lists, int groupItemsCount);
    }
}
