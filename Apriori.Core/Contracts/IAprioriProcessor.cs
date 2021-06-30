using System.Collections.Generic;

namespace Apriori.Core
{
    public interface IAprioriProcessor
    {
        List<AssociationRule> GetRules(ItemSetList itemSets, int minSup);
        AssociationRuleModel PrepareAssociationModel(ItemSetList itemSets, int minSup);
    }
}
