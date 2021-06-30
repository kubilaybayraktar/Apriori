using System;
using System.Collections.Generic;
using System.Linq;

namespace Apriori.Core
{
    public class AssociationProcessor : IAssociationProcessor
    {
        #region Props
        IEnumerable<ItemGroup> _groups;
        #endregion

        #region Methods
        /// <summary>
        /// Get Association Rules
        /// </summary>
        /// <param name="associationRuleModel">Association Model</param>
        /// <returns>List of Association Rules</returns>
        public List<AssociationRule> GetRules(AssociationRuleModel associationRuleModel)
        {
            if (associationRuleModel == null)
                throw new ArgumentNullException(nameof(associationRuleModel));

            if (associationRuleModel.ItemSetList == null)
                throw new ArgumentNullException(nameof(associationRuleModel.ItemSetList));

            List<AssociationRule> rules = new();
            _groups = CreateItemGroupList(associationRuleModel.ItemSetList);

            foreach (ItemGroup group in _groups)
            {
                if (group.Labels.Count < 2 || group.Value < associationRuleModel.MinSupport)
                    continue;

                int labelsCount = group.Labels.Count;
                for (int i = 1; i < labelsCount; i++)
                {
                    string key1 = ToRuleString(group.Labels.Take(i).ToList());
                    AddRule(rules, group, key1, i, associationRuleModel.Total, true);

                    string key2 = ToRuleString(group.Labels.Skip(i).Take(labelsCount - i).ToList());
                    AddRule(rules, group, key2, i, associationRuleModel.Total, false);
                }
            }

            return rules;
        }

        private void AddRule(List<AssociationRule> rules, ItemGroup group, string key, int i, int total, bool skip)
        {
            int labelsCount = group.Labels.Count;
            ItemGroup firstItem = _groups.FirstOrDefault(x => x.Key == key);

            if (firstItem != null)
            {
                string toItem = skip ? ToRuleString(group.Labels.Skip(i).Take(labelsCount - i).ToList()):
                                        ToRuleString(group.Labels.Take(i).ToList());

                rules.Add(CreateAssociationRule(firstItem.Value, group.Value, total, firstItem.Key, toItem));
            }
        }

        private AssociationRule CreateAssociationRule(int value, int groupValue, int total, string from, string to)
        {
            double support = Math.Round((double)groupValue / total * 100, 2);
            double confidence = Math.Round((double)groupValue / value * 100, 2);
            return new AssociationRule
            {
                Label = $"{from} => {to}",
                Support = support,
                Confidance = confidence
            };
        }

        public IEnumerable<ItemGroup> CreateItemGroupList(ItemSetList itemSetList)
        {
            foreach (ItemSet itemSet in itemSetList)
            {
                string rule = ToRuleString(itemSet.Labels);
                yield return NewItemGroup(rule, itemSet.Labels, itemSet.First().Value);
            }
        }

        private ItemGroup NewItemGroup(string key, List<string> labels, int value)
        {
            return new ItemGroup
            {
                Key = key,
                Labels = labels,
                Value = value
            };
        }

        private string ToRuleString(List<string> list)
        {
            string str = "";
            for (int i = 0; i < list.Count; i++)
            {
                str += str == "" ? list[i] : ", " + list[i];
            }

            return str;
        }
        #endregion
    }
}
