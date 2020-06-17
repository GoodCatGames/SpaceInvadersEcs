using System.Collections.Generic;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Events.InputEvents;
using SpaceInvadersLeoEcs.Helpers;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Controller
{
    internal sealed class InputShootSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InputShootStartedEvent> _filterShootStarted = null;
        private readonly EcsFilter<InputShootCanceledEvent> _filterShootCanceled = null;
        
        private readonly EcsFilter<IsCanShootComponent, OwnerComponent> _filterGuns = null;
        
        private readonly HashSet<int> _numberPlayersIsShooting = new HashSet<int>(); 
        
        public void Run()
        {
            foreach (var i in _filterShootStarted)
            {
                var inputShootStartedEvent = _filterShootStarted.Get1(i);
                var playerNumber = inputShootStartedEvent.PlayerNumber;
                ProcessShootEvent(playerNumber, true);
                _numberPlayersIsShooting.Add(playerNumber);
            }
            
            foreach (var i in _filterShootCanceled)
            {
                var inputShootCanceledEvent = _filterShootCanceled.Get1(i);
                var playerNumber = inputShootCanceledEvent.PlayerNumber;
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

        private void MakeShooting(ref EcsEntity gun, Vector2 direction) => gun.Replace(new Shooting() {Direction = direction});

        private void CancelShooting(ref EcsEntity gun) => gun.Del<Shooting>();
    }
}