using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Components.Events;

namespace SpaceInvadersLeoEcs.Systems.Model.Move
{
    internal sealed class MoveSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<MoveComponent, ViewObjectComponent> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var moveComponent = ref _filter.Get1(i);
                ref var viewObjectComponent = ref _filter.Get2(i);
                
                viewObjectComponent.ViewObject.MoveTo(moveComponent.Direct * moveComponent.Speed); 
                var positionNew = viewObjectComponent.ViewObject.Position;

                ref var entity = ref _filter.GetEntity(i);
                entity.Get<ChangePositionEvent>().positionNew = positionNew;
            }
        }
    }
}