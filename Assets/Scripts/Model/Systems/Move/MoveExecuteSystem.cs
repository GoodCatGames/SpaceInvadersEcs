using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Events;

namespace Model.Systems.Move
{
    public sealed class MoveExecuteSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<global::Model.Components.Body.Move, ViewObjectComponent> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var moveComponent = ref _filter.Get1(i);
                ref var viewObjectComponent = ref _filter.Get2(i);
                
                viewObjectComponent.ViewObject.MoveTo(moveComponent.Direct * moveComponent.Speed); 
                var positionNew = viewObjectComponent.ViewObject.Position;

                ref var entity = ref _filter.GetEntity(i);
                entity.Get<PositionChangeEvent>().positionNew = positionNew;
            }
        }
    }
}