using System.Collections.Generic;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Events.InputEvents;
using SpaceInvadersLeoEcs.Helpers;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Controller
{
    internal sealed class ShootInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputShootStartedEvent> _filterShootStarted = null;
        private readonly EcsFilter<InputShootCanceledEvent> _filterShootCanceled = null;
        private readonly EcsFilter<IsCanShootComponent, OwnerPlayerComponent> _filterGuns = null;
        
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

        private void ProcessShootEvent(int numberPlayer, bool isPressed)
        {
            var gun = _filterGuns.GetGunOfPlayer(numberPlayer);
            
            if (isPressed)
            {
                MakeShooting(ref gun, Vector2.up);
            }
            else
            {
                CancelShooting(ref gun);
            }
        }

        private void MakeShooting(ref EcsEntity gun, Vector2 direction) => gun.Get<ShootingComponent>().Direction = direction;

        private void CancelShooting(ref EcsEntity gun) => gun.Del<ShootingComponent>();
    }
}