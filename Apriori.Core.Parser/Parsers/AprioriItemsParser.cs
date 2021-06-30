using System;
using System.Collections.Generic;
using System.Linq;

namespace Apriori.Core
{
    public class AprioriItemsParser : IAprioriItemsParser
    {
        private const string GroupSeparator = "_";
        private const string ItemSeparator = "-";

        /// <summary>
        /// Parse input
        /// </summary>
        /// <param name="input">Input String</param>
        /// <returns>Item set list</returns>
        public ItemSetList Process(string input)
        {
            //Validation
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            //Prepare
            ItemSetList result = new();

            //Get Groups
            List<string> groups = GetGroups(input);

            //Get Items & Create Result
            groups.ForEach(x => result.Add(GetItemSet(x)));

            return result;
        }

        private List<string> GetGroups(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            return input.Split(GroupSeparator).ToList();
        }

        private ItemSet GetItemSet(string input)
        {
            //Prepare
            ItemSet itemSet = new();

            //Get Items
            string[] items = input.Split(ItemSeparator);

            //Create ItemSet
            itemSet.Add(items.ToList(), 0);

            return itemSet;
        }
    }
}