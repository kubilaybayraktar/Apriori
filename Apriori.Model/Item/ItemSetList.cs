using System.Collections.Generic;
using System.Linq;

namespace Apriori.Core
{
    public class ItemSetList : List<IItemSet>, IItemSetList
    {
        public ItemSetList CalculateCount(IEnumerable<IEnumerable<IItem>> relations, int minSup)
        {
            ItemSetList result = new();

            foreach (IEnumerable<IItem> relation in relations)
            {
                int count = 0;
                foreach (ItemSet itemSet in this)
                {
                    bool exists = false;
                    foreach (Item relItem in relation)
                    {
                        if (itemSet.Labels.Contains(relItem.Label))
                        {
                            exists = true;
                        }
                        else
                        {
                            exists = false;
                            break;
                        }
                    }

                    if (exists)
                    {
                        count++;
                    }
                }

                if (count > 0 && count >= minSup)
                {
                    ItemSet newItemSet = new();
                    newItemSet.Add(ToLabels(relation).ToList(), count);

                    result.Add(newItemSet);
                }
            }

            return result;
        }

        public IEnumerable<string> ToLabels(IEnumerable<IItem> items)
        {
            foreach (IItem item in items)
                yield return item.Label;
        }
    }
}
