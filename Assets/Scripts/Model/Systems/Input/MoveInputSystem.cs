using System;
using Leopotam.Ecs;
using Model.AppData;
using Model.Components.Body;
using Model.Components.Events.InputEvents;
using UnityEngine;

namespace Model.Systems.Input
{
    public sealed class MoveInputSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly GameConfiguration _gameConfiguration = null;

        private readonly EcsFilter<InputMoveStartedEvent> _filterMoveStart = null;
        private readonly EcsFilter<InputMoveCanceledEvent> _filterMoveCanceled = null;
        private readonly EcsFilter<Player, Components.Body.Move> _filterMove = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterMoveStart)
            {
                ref var inputMoveStartedEvent = ref _filterMoveStart.Get1(i);
                var direction = GetDirection(inputMoveStartedEvent.Axis);
                ProcessMove(inputMoveStartedEvent.PlayerNumber, true, direction);
            }

            foreach (var i in _filterMoveCanceled)
            {
                ref var inputMoveCanceledEvent = ref _filterMoveCanceled.Get1(i);
                ProcessMove(inputMoveCanceledEvent.PlayerNumber, false);
            }
        }

        private void ProcessMove(in int numberPlayer, in bool doMove, in Vector2 direction = new Vector2())
        {
            foreach (var i in _filterMove)
            {
                if (IsPlayerWithNumber(numberPlayer, i) == false)
                    continue;

                var speedPlayer = GetSpeedPlayer(numberPlayer);

                ref var move = ref _filterMove.Get2(i);
                if (doMove)
                {
                    MovePlayer(ref move, direction, speedPlayer);
                }
                else
                {
                    StopPlayer(ref move);
                }
            }
        }

        private bool IsPlayerWithNumber(in int numberPlayer, in int indexFilter)
        {
            var playerComponent = _filterMove.Get1(indexFilter);
            return playerComponent.Number == numberPlayer;
        }

        private float GetSpeedPlayer(in int numberPlayer)
        {
            if (numberPlayer == 1) return _gameConfiguration.Player1Speed;
            if (numberPlayer == 2) return _gameConfiguration.Player2Speed;
            throw new Exception();
        }

        private void MovePlayer(ref Components.Body.Move component, in Vector2 direction, in float speed)
        {
            component.Speed = speed;
            component.Direct = direction;
        }

        private void StopPlayer(ref Components.Body.Move component)
        {
            component.Speed = 0;
        }

        private Vector2 GetDirection(in float axis) => axis >= 0 ? Vector2.right : Vector2.left;
    }
}