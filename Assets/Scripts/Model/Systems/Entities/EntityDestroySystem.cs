using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Requests;

namespace Model.Systems.Entities
{
    public sealed class EntityDestroySystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<ViewObjectComponent, EntityDestroyRequest> _filterWithView = null;
        private readonly EcsFilter<EntityDestroyRequest> _filterWithoutView = null;

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