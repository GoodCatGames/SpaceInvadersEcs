using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Player;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Model.Move
{
    internal sealed class PlayerMoveOutBorderSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly GameContext _gameContext = null;

        private readonly EcsFilter<MoveComponent, ViewObjectComponent, PlayerComponent> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var moveComponent = ref _filter.Get1(i);
                ref var viewObjectComponent =ref _filter.Get2(i);

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
            if (position.x >= _gameContext.MaxBorderGameField.x)
            {
                borderPosition.x = _gameContext.MaxBorderGameField.x;
                borderPosition.x -= delta;
                isOutBorder = true;
            }

            if (position.y >= _gameContext.MaxBorderGameField.y)
            {
                borderPosition.y = _gameContext.MaxBorderGameField.y;
                borderPosition.y -= delta;
                isOutBorder = true;
            }

            if (position.x <= _gameContext.MinBorderGameField.x)
            {
                borderPosition.x = _gameContext.MinBorderGameField.x;
                borderPosition.x += delta;
                isOutBorder = true;
            }

            if (position.y <= _gameContext.MinBorderGameField.y)
            {
                borderPosition.y = _gameContext.MinBorderGameField.y;
                borderPosition.y += delta;
                isOutBorder = true;
            }
            return isOutBorder;
        }
    }
}