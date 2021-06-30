using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apriori.Core
{
    public static class Extensions
    {
        /// <summary>
        /// Get Distinct Values of ItemSet List
        /// </summary>
        /// <param name="itemSets"ItemSet List</param>
        /// <returns></returns>
        public static IEnumerable<IItem> ToDistinctValues(this List<IItemSet> itemSets)
        {
            List<IItem> singleItems = new();

            //Create Single Items List
            foreach (ItemSet itemSet in itemSets)
            {
                foreach (List<string> keys in itemSet.Keys)
                {
                    foreach (string key in keys)
                    {
                        if (!singleItems.Any(x => x.Label == key))
                        {
                            singleItems.Add(new Item
                            {
                                Label = key,
                                Count = 0
                            });
                        }
                    }
                }
            }

            //Calculate Count
            CalculateSingleItemsCount(itemSets, singleItems);

            return singleItems.OrderBy(x => x).ToList();
        }

        /// <summary>
        /// Calculate Single Item Counts
        /// </summary>
        /// <param name="itemSets">Item Set</param>
        private static void CalculateSingleItemsCount(List<IItemSet> itemSets, List<IItem> singleItems)
        {
            int count;
            foreach (IItem singleItem in singleItems)
            {
                count = 0;
                foreach (IItemSet itemSet in itemSets)
                {
                    foreach (List<string> listItems in itemSet.Keys)
                    {
                        if (listItems.Any(x => x == singleItem.Label))
                        {
                            count++;
                            break;
                        }
                    }
                }

                singleItem.Count = count;
            }
        }

        /// <summary>
        /// Convert Association Rules to Readable Strings
        /// </summary>
        /// <param name="rules">Association Rules</param>
        /// <returns></returns>
        public static string ToLines(this List<AssociationRule> rules)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var rule in rules.OrderByDescending(x=>x.Confidance))
            {
                sb.Append(rule.Label).Append($" Confidence:{rule.Confidance} Support:{rule.Support}");
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        public static int GetMaxGroupItemsCount(this List<IItemSet> itemSets)
        {
            int max = 0;
            itemSets.ForEach(x =>
            {
                if (x.ItemsCount > max)
                    max = x.ItemsCount;
            });

            return max;
        }

        public static int GetMinGroupItemsCount(this List<IItemSet> itemSets)
        {
            int min = short.MaxValue;
            itemSets.ForEach(x =>
            {
                if (x.ItemsCount < min)
                    min = x.ItemsCount;
            });

            return min;
        }

        public static IEnumerable<string> ToLabels(this IEnumerable<IItem> items)
        {
            foreach (IItem item in items)
                yield return item.Label;
        }
    }
}
