namespace Apriori.Core
{
    public class Item : IItem
    {
        public string Label { get; set; }
        public int Count { get; set; }

        public int CompareTo(object obj)
        {
            Item item = (Item)obj;
            if (item == null) return 1;

            return Label.CompareTo(item.Label);
        }
    }
}
