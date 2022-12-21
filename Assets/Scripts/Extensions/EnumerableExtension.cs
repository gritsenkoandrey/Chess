using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OnlineChess.Extensions
{
    public static class EnumerableExtension
    {
        public static T PickRandom<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return default;
            }

            if (collection is IList<T> list)
            {
                return list.Count != 0 ? list[Random.Range(0, list.Count)] : default;
            }

            if (collection is HashSet<T> hashset)
            {
                return hashset.Count != 0 ? hashset.ElementAt(Random.Range(0, hashset.Count)) : default;
            }

            List<T> actual = collection.ToList();
        
            return actual.Count > 0 ? actual[Random.Range(0, actual.Count)] : default;
        }
    }
}