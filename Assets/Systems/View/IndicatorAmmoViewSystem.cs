﻿using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.Player;
using SpaceInvadersLeoEcs.Components.Body.UI;
using SpaceInvadersLeoEcs.Components.Events;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Services;
using SpaceInvadersLeoEcs.Extensions.Enitities;
using SpaceInvadersLeoEcs.Helpers;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.Systems.View
{
    internal sealed class IndicatorAmmoViewSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly AudioService _audioService = null;

        private readonly EcsFilter<IsCanShootComponent, OwnerComponent, IsReloadStartEvent> _gunsStartReload = null;
        private readonly EcsFilter<IsCanShootComponent, OwnerComponent, IsReloadEndEvent> _gunsEndReload = null;
        private readonly EcsFilter<IsCanShootComponent, OwnerComponent, IsShotMadeEvent> _gunsMadeShot = null;

        private readonly EcsFilter<WrapperUnityObject<Text>, OwnerComponent, IsGunIndicatorComponent>
            _indicators = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _gunsMadeShot)
            {
                var gun = _gunsMadeShot.GetEntity(i);
                SetAmmoState(gun);
            }

            foreach (var i in _gunsStartReload)
            {
                var gun = _gunsStartReload.GetEntity(i);
                SetReloadState(gun);

                var playerOwner = gun.Get<OwnerComponent>();
                var numberPlayer = playerOwner.Entity.Get<PlayerComponent>().Number;
                _audioService.StartPlayReloadPlayer(numberPlayer);
            }

            foreach (var i in _gunsEndReload)
            {
                var gun = _gunsEndReload.GetEntity(i);
                SetAmmoState(gun);
                
                var playerOwner = gun.Get<OwnerComponent>();
                var numberPlayer = playerOwner.Entity.Get<PlayerComponent>().Number;
                
                _audioService.StopPlayReload(numberPlayer);
            }
        }


        private void SetReloadState(EcsEntity gun)
        {
            var indicator = GetIndicator(gun);
            if (indicator == null) return;
            indicator.text = "RELOAD";
        }

        private void SetAmmoState(EcsEntity gun)
        {
            var indicator = GetIndicator(gun);
            if (indicator == null) return;
            gun.TryGet(out Ammo ammo);
            var ammoCapacity = gun.Get<AmmoCapacity>();
            SetAmmoState(indicator, ammo.Value, ammoCapacity.Value);
        }

        private void SetAmmoState(Text indicator, int ammo, int ammoCapacity)
        {
            indicator.text = $"{ammo} / {ammoCapacity}";
        }
       
        private Text GetIndicator(EcsEntity gun)
        {
            var ownerComponent = gun.Get<OwnerComponent>();
            var indicator = GetIndicator(ownerComponent);
            return indicator;
        }

        private Text GetIndicator(OwnerComponent ownerComponent) =>
            _indicators.GetIndicator(ownerComponent.Entity.Get<PlayerComponent>().Number);
    }
}