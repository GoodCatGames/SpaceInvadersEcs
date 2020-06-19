using Leopotam.Ecs;

namespace SpaceInvadersLeoEcs.Extensions.UnityComponents
{
    public interface IEcsUnityProvider
    {
        ref EcsEntity Entity { get; }

        void SetEntity(in EcsEntity entity);
    }
}