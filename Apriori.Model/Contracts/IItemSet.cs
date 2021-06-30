using System.Collections.Generic;

namespace Apriori.Core
{
    public interface IItemSet : IDictionary<List<string>, int>
    {
        int ItemsCount { get; }
        List<string> Labels { get; }
        double Support { get; set; }
        double Confidence { get; set; }
    }
}
