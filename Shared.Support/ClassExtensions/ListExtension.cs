using System;
using System.Collections.Generic;

namespace Shared.Support.ClassExtensions
{
    public static class ListExtension
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        //public static IDictionary<TSource, TSource> ExceptToDictonary<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        //{
        //    IDictionary<TSource, TSource> keyValuePairs = new Dictionary<TSource, TSource>();

        //    HashSet<TSource> set = new HashSet<TSource>(comparer);

        //    foreach (TSource element in first) set.Add(element);

        //    foreach (TSource element in second)
        //    {
        //        if()
        //    }
            

        //    return keyValuePairs;
        //}
    }
}
