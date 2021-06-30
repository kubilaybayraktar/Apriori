using System.Collections.Generic;
using System.Linq;

namespace Apriori.Core
{
    public class ItemSet : Dictionary<List<string>, int>, IItemSet
    {
        public int ItemsCount => Keys.First().Count;
        public List<string> Labels => Keys.First();
        public double Support { get; set; }
        public double Confidence { get; set; }
        public override string ToString()
        {
            return string.Join(", ", Keys);
        }
    }
}
