using System;

namespace Apriori.Core
{
    public interface IItem : IComparable
    {
        string Label { get; set; }
        int Count { get; set; }
    }
}
