using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Components.Requests;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class DestroyEntitySystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<ViewObjectComponent, DestroyEntityRequest> _filterWithView = null;
        private readonly EcsFilter<DestroyEntityRequest> _filterWithoutView = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterWithView)
            {
                var viewObjectComponent = _filterWithView.Get1(i);
                viewObjectComponent.ViewObject.Destroy();
            }

            foreach (var i in _filterWithoutView)
            {
                var entity = _filterWithoutView.GetEntity(i);
                entity.Destroy();
            }
        }
    }
}