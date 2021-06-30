namespace Apriori.Core
{
    public class AssociationRuleModel
    {
        public ItemSetList ItemSetList { get; set; }
        public int Total { get; set; }
        public int MinSupport { get; set; }
    }
}
