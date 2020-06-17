using Leopotam.Ecs;

namespace SpaceInvadersLeoEcs.Extensions
{
    public static class FilterLinqExtension
    {
        private delegate T GetComponentDelegate<out T>(int i);

        public static EcsEntity[] GetEntitiesToArray(this EcsFilter filter) => GetToArray(filter, i => filter.GetEntity(i));

        #region Get1

        public static T[] Get1ToArray<T>(this EcsFilter<T> filter)
            where T : struct => GetToArray(filter, i => filter.Get1(i));

        public static T1[] Get1ToArray<T1, T2>(this EcsFilter<T1, T2> filter)
            where T1 : struct
            where T2 : struct => GetToArray(filter, i => filter.Get1(i));

        public static T1[] Get1ToArray<T1, T2, T3>(this EcsFilter<T1, T2, T3> filter)
            where T1 : struct
            where T2 : struct
            where T3 : struct => GetToArray(filter, i => filter.Get1(i));

        public static T1[] Get1ToArray<T1, T2, T3, T4>(this EcsFilter<T1, T2, T3, T4> filter)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct => GetToArray(filter, i => filter.Get1(i));

        #endregion

        #region Get2

        public static T2[] Get2ToArray<T1, T2>(this EcsFilter<T1, T2> filter)
            where T1 : struct
            where T2 : struct => GetToArray(filter, i => filter.Get2(i));

        public static T2[] Get2ToArray<T1, T2, T3>(this EcsFilter<T1, T2, T3> filter)
            where T1 : struct
            where T2 : struct
            where T3 : struct => GetToArray(filter, i => filter.Get2(i));

        public static T2[] Get2ToArray<T1, T2, T3, T4>(this EcsFilter<T1, T2, T3, T4> filter)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct => GetToArray(filter, i => filter.Get2(i));

        #endregion

        #region Get3

        public static T3[] Get3ToArray<T1, T2, T3>(this EcsFilter<T1, T2, T3> filter)
            where T1 : struct
            where T2 : struct
            where T3 : struct => GetToArray(filter, i => filter.Get3(i));

        public static T3[] Get3ToArray<T1, T2, T3, T4>(this EcsFilter<T1, T2, T3, T4> filter)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct => GetToArray(filter, i => filter.Get3(i));

        #endregion

        #region Get4

        public static T4[] Get4ToArray<T1, T2, T3, T4>(this EcsFilter<T1, T2, T3, T4> filter)
            where T1 : struct
            where T2 : struct
            where T3 : struct
            where T4 : struct => GetToArray(filter, i => filter.Get4(i));

        #endregion

        private static T[] GetToArray<T>(EcsFilter filter, GetComponentDelegate<T> getComponentDelegate)
            where T : struct
        {
            var array = new T[filter.GetEntitiesCount()];
            foreach (var i in filter)
            {
                var get1 = getComponentDelegate.Invoke(i);
                array[i] = get1;
            }

            return array;
        }
    }
}