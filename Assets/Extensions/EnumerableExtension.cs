using System.Collections.Generic;
using System.Linq;

namespace SpaceInvadersLeoEcs.Extensions
{
    public static class EnumerableExtension
    {
        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();
            if (!array.Any()) return default;
            var randomIndex = UnityEngine.Random.Range(0, array.Count());
            return array[randomIndex];
        }
    }
}