using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Player;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Model.Move
{
    sealed class MoveBorderPlayerSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly GameContext _gameContext = null;

        private readonly EcsFilter<MoveComponent, ViewObjectComponent, PlayerComponent> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var moveComponent = ref _filter.Get1(i);
                var viewObjectComponent = _filter.Get2(i);

                var position = viewObjectComponent.ViewObject.Position;
                if (IsOutBorder(position, out var borderPosition))
                {
                    viewObjectComponent.ViewObject.Position = borderPosition;
                    moveComponent.Speed = 0;
                }
            }
        }

        private bool IsOutBorder(Vector2 position, out Vector2 borderPosition)
        {
            var delta = 0.01f;
            borderPosition = position;
            var isOutBorder = false;
            if (position.x >= _gameContext.MaxBorderScreen.x)
            {
                borderPosition.x = _gameContext.MaxBorderScreen.x;
                borderPosition.x -= delta;
                isOutBorder = true;
            }

            if (position.y >= _gameContext.MaxBorderScreen.y)
            {
                borderPosition.y = _gameContext.MaxBorderScreen.y;
                borderPosition.y -= delta;
                isOutBorder = true;
            }

            if (position.x <= _gameContext.MinBorderScreen.x)
            {
                borderPosition.x = _gameContext.MinBorderScreen.x;
                borderPosition.x += delta;
                isOutBorder = true;
            }

            if (position.y <= _gameContext.MinBorderScreen.y)
            {
                borderPosition.y = _gameContext.MinBorderScreen.y;
                borderPosition.y += delta;
                isOutBorder = true;
            }
            return isOutBorder;
        }
    }
}