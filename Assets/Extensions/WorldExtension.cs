using Leopotam.Ecs;

namespace SpaceInvadersLeoEcs.Extensions
{
    public static class WorldExtension
    {
        public static void SendMessage<T>(this EcsWorld world, T messageEvent)
            where T : struct
        {
            var entity = world.NewEntity();
            entity.Replace(messageEvent);
        }
    }
}