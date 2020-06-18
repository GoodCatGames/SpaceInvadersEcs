﻿using System;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Player;
using SpaceInvadersLeoEcs.Components.Events.InputEvents;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Controller
{
    internal sealed class InputMoveSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<InputMoveStartedEvent> _filterMoveStart = null;
        private readonly EcsFilter<InputMoveCanceledEvent> _filterMoveCanceled = null;
        private readonly EcsFilter<PlayerComponent, MoveComponent> _filterMove = null;
        
        private readonly GameConfiguration _gameConfiguration = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterMoveStart)
            {
                var inputMoveStartedEvent = _filterMoveStart.Get1(i);
                var direction = GetDirection(inputMoveStartedEvent.Axis);
                ProcessMove(inputMoveStartedEvent.PlayerNumber, true, direction);
            }
            
            foreach (var i in _filterMoveCanceled)
            {
                var inputMoveCanceledEvent = _filterMoveCanceled.Get1(i);
                ProcessMove(inputMoveCanceledEvent.PlayerNumber, false);
            }
        }
        
        private void ProcessMove(int numberPlayer, bool doMove, Vector2 direction = new Vector2())
        {
            foreach (var i in _filterMove)
            {
                if (!IsPlayerWithNumber(numberPlayer, i)) continue;

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

        private bool IsPlayerWithNumber(int numberPlayer, int indexFilter)
        {
            var playerComponent = _filterMove.Get1(indexFilter);
            return playerComponent.Number == numberPlayer;
        }

        private float GetSpeedPlayer(int numberPlayer)
        {
            if (numberPlayer == 1) return _gameConfiguration.Player1Speed;
            if (numberPlayer == 2) return _gameConfiguration.Player2Speed;
            throw new Exception();
        }
        
        private void MovePlayer(ref MoveComponent component, Vector2 direction, float speed)
        {
            component.Speed = speed;
            component.Direct = direction;
        }

        private void StopPlayer(ref MoveComponent component)
        {
            component.Speed = 0;
        }

        private Vector2 GetDirection(float axis) => axis >= 0 ? Vector2.right : Vector2.left;

    }
}