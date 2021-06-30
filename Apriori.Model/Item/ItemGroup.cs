using System.Collections.Generic;

namespace Apriori.Core
{
    public class ItemGroup
    {
        public string Key { get; set; }
        public List<string> Labels { get; set; }
        public int Value { get; set; }
    }
}
