using Leopotam.Ecs;

namespace SpaceInvadersLeoEcs.Extensions.UnityComponents
{
    public interface IEcsUnityProvider
    {
        ref EcsEntity Entity { get; }
        ref EcsWorld World { get; }
        
        void SetEntity(in EcsWorld world, in EcsEntity entity);
        void SetWorld(in EcsWorld world);
    }
}