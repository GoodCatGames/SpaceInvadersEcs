using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Components.Events.UnityEvents;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Services;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    public class OnBecameInvisibleSystem : IEcsRunSystem
    {
        private PoolsObject _poolsObject;
        private readonly EcsFilter<ViewObjectComponent, ContainerComponents<OnBecameInvisibleEvent>> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var viewObjectComponent = _filter.Get1(i);
                viewObjectComponent.ViewObject.Destroy();
                var entity = _filter.GetEntity(i);
                entity.Destroy();
            }
        }
    }
}