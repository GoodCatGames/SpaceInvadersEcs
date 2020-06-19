using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Components.Requests;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class EntityDestroySystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<ViewObjectComponent, IsDestroyEntityRequest> _filterWithView = null;
        private readonly EcsFilter<IsDestroyEntityRequest> _filterWithoutView = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterWithView)
            {
                _filterWithView.Get1(i).ViewObject.Destroy();
            }

            foreach (var i in _filterWithoutView)
            {
                _filterWithoutView.GetEntity(i).Destroy();
            }
        }
    }
}