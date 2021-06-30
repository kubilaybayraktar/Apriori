using System.Collections.Generic;

namespace Apriori.Core
{
    public class AprioriProcessor : IAprioriProcessor
    {
        #region Props
        private readonly IRelationsProcessor _relationsProcessor;
        private readonly IAssociationProcessor _associationProcessor;
        #endregion

        #region Ctor
        public AprioriProcessor(IRelationsProcessor relationsProcessor, IAssociationProcessor associationProcessor)
        {
            _relationsProcessor = relationsProcessor;
            _associationProcessor = associationProcessor;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get Association Rules
        /// </summary>
        /// <param name="itemSets">Item Set List</param>
        /// <param name="minSup">Min. Support Value</param>
        /// <returns></returns>
        public List<AssociationRule> GetRules(ItemSetList itemSets, int minSup)
        {
            //Get Relations & Prepare Association Model
            AssociationRuleModel associationRuleModel = PrepareAssociationModel(itemSets, minSup);

            //Get Rules
            List<AssociationRule> rules = _associationProcessor.GetRules(associationRuleModel);

            return rules;
        }

        /// <summary>
        /// Calculate group usage count and Prepare Association Rule Model
        /// </summary>
        /// <param name="itemSets">Item set list</param>
        /// <param name="minSup">Min. Support Value</param>
        /// <returns></returns>
        public AssociationRuleModel PrepareAssociationModel(ItemSetList itemSets, int minSup)
        {
            if (minSup <= 0)
                throw new System.Exception($"The parameter must be greater than 0 : {nameof(minSup)}");

            //Get Threshold Values
            ItemSetList result = new();
            int minGroupItemsCount = itemSets.GetMinGroupItemsCount();
            int maxGroupItemsCount = itemSets.GetMaxGroupItemsCount();

            //Get Single Items
            IEnumerable<IItem> _singleItems = itemSets.ToDistinctValues();

            if (minGroupItemsCount > 0)
            {
                for (int i = 1; i <= maxGroupItemsCount; i++)
                {
                    IEnumerable<IEnumerable<IItem>> relations = _relationsProcessor.GetRelations(_singleItems, i);

                    result.AddRange(itemSets.CalculateCount(relations, minSup));
                }
            }

            //Prepare Model
            return new AssociationRuleModel { ItemSetList = result, Total = itemSets.Count, MinSupport = minSup };
        }
        #endregion
    }
}
