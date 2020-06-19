using System;
using System.Collections.Generic;

namespace SpaceInvadersLeoEcs.Extensions
{
    public static class ListExtension
    {
        public static T Random<T>(this List<T> list)
        {
            var listCount = list.Count;
            if (listCount == 0) return default;
            var randomIndex = UnityEngine.Random.Range(0, listCount);
            return list[randomIndex];
        }
    }
}