using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Components.Events;

namespace SpaceInvadersLeoEcs.Systems.Model.Move
{
    sealed class MoveSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<MoveComponent, ViewObjectComponent> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                var moveComponent = _filter.Get1(i);
                var viewObjectComponent = _filter.Get2(i);
                
                var positionOld = viewObjectComponent.ViewObject.Position;
                viewObjectComponent.ViewObject.MoveTo(moveComponent.Direct * moveComponent.Speed); 
                var positionNew = viewObjectComponent.ViewObject.Position;

                var entity = _filter.GetEntity(i);
                entity.Replace(new ChangePositionEvent() {positionOld = positionOld, positionNew = positionNew});
            }
        }
    }
}