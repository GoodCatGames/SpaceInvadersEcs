using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Components.Events.UnityEvents;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Services;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    public class UnityEventOnBecameInvisibleSystem : IEcsRunSystem
    {
        private readonly PoolsObject _poolsObject;
        private readonly EcsFilter<ViewObjectComponent, ContainerComponents<OnBecameInvisibleEvent>> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                _filter.GetEntity(i).Get<IsDestroyEntityRequest>();
            }
        }
    }
}