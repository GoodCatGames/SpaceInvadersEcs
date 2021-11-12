using System.Collections.Generic;
using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Body.Gun;
using Model.Components.Events.InputEvents;
using UnityEngine;

namespace Model.Systems.Input
{
    public sealed class ShootInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputShootStartedEvent> _filterShootStarted = null;
        private readonly EcsFilter<InputShootCanceledEvent> _filterShootCanceled = null;
        private readonly EcsFilter<ShootIsPossible, PlayerOwner> _filterGuns = null;

        private readonly HashSet<int> _numberPlayersIsShooting = new HashSet<int>();

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterShootStarted)
            {
                var playerNumber = _filterShootStarted.Get1(i).PlayerNumber;
                ProcessShootEvent(playerNumber, true);
                _numberPlayersIsShooting.Add(playerNumber);
            }

            foreach (var i in _filterShootCanceled)
            {
                ref var inputShootCanceledEvent = ref _filterShootCanceled.Get1(i);
                ref var playerNumber = ref inputShootCanceledEvent.PlayerNumber;
                ProcessShootEvent(playerNumber, false);
                _numberPlayersIsShooting.Remove(playerNumber);
            }

            foreach (var i in _numberPlayersIsShooting)
            {
                ProcessShootEvent(i, true);
            }
        }

        private void ProcessShootEvent(in int numberPlayer, in bool isPressed)
        {
            var gun = GetGunOfPlayer(_filterGuns, numberPlayer);

            if (isPressed)
            {
                MakeShooting(ref gun, Vector2.up);
            }
            else
            {
                CancelShooting(ref gun);
            }
        }

        private void MakeShooting(ref EcsEntity gun, in Vector2 direction) => gun.Get<Shooting>().Direction = direction;

        private void CancelShooting(ref EcsEntity gun) => gun.Del<Shooting>();

        private EcsEntity GetGunOfPlayer(EcsFilter<ShootIsPossible, PlayerOwner> guns,
            in int playerNumber)
        {
            foreach (var i in guns)
            {
                ref var ownerPlayerComponent = ref guns.Get2(i);
                var ownerPlayer = ownerPlayerComponent.PlayerEntity;
                ref var playerComponent = ref ownerPlayer.Get<Player>();
                if (playerComponent.Number == playerNumber)
                {
                    return guns.GetEntity(i);
                }
            }

            return EcsEntity.Null;
        }
    }
}