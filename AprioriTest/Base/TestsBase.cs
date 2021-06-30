using System.Collections.Generic;
using System.Linq;

namespace Apriori.Core
{
    public abstract class TestsBase
    {
        protected readonly IAprioriProcessor _aprioriCoreProcessor;
        protected readonly IAprioriItemsParser _aprioriItemsParser;
        protected readonly IRelationsProcessor _relationsProcessor;
        protected readonly IAssociationProcessor _associationProcessor;
        internal string ValidInput = "1-2-3-5_2-3-4_1-3-4_2-5_3-2_1-3";
        internal List<string> SingleItems = new List<string> { "1", "2", "3", "4", "5" };
        internal string EmptyInput = "";
        internal ushort MinSup = 2;
        internal int countOfLabel5 = 2;

        public TestsBase()
        {
            _relationsProcessor = new RelationsProcessor();
            _aprioriItemsParser = new AprioriItemsParser();
            _associationProcessor = new AssociationProcessor();
            _aprioriCoreProcessor = new AprioriProcessor(_relationsProcessor, _associationProcessor);
        }

        public int GetCountOfLabel(List<IItem> result, string label)
        {
            return result.First(x => x.Label == label).Count;
        }

        public ItemSetList GetItemSet(string input)
        {
            return _aprioriItemsParser.Process(input);
        }
    }
}
