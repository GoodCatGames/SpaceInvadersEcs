using Leopotam.Ecs;

namespace SpaceInvadersLeoEcs.Extensions.Enitities
{
    public static class EntityExtension
    {
        public static bool TryGet<T>(this EcsEntity entity, out T component)
            where T : struct
        {
            component = default;
            if (entity.Has<T>())
            {
                ref var foo = ref entity.Get<T>();
                component = foo;
                return true;
            }

            return false;
        }
    }
}