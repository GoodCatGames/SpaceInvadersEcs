using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Components.Events.UnityEvents;
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
                _filter.Get1(i).ViewObject.Destroy();
                _filter.GetEntity(i).Destroy();
            }
        }
    }
}