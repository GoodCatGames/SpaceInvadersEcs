using Leopotam.Ecs;

namespace SpaceInvadersLeoEcs.UnityComponents
{
    public interface IEcsUnityProvider
    {
        EcsEntity Entity { get; }
        EcsWorld World { get; }
        
        void SetEntity(in EcsWorld world, in EcsEntity entity);
        void SetWorld(in EcsWorld world);
    }
}