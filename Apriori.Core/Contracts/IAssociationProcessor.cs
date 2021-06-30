using System.Collections.Generic;

namespace Apriori.Core
{
    public interface IAssociationProcessor
    {
        List<AssociationRule> GetRules(AssociationRuleModel model);
    }
}
