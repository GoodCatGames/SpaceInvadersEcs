using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.Player;
using SpaceInvadersLeoEcs.Components.Body.UI;
using SpaceInvadersLeoEcs.Components.Events;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Services;
using SpaceInvadersLeoEcs.Helpers;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.Systems.View
{
    internal sealed class GunIndicatorViewUpdateSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly AudioService _audioService = null;

        private readonly EcsFilter<IsCanShootComponent, OwnerPlayerComponent, IsReloadStartEvent> _gunsStartReload = null;
        private readonly EcsFilter<IsCanShootComponent, OwnerPlayerComponent, IsReloadEndEvent> _gunsEndReload = null;
        private readonly EcsFilter<IsCanShootComponent, OwnerPlayerComponent, IsShotMadeEvent> _gunsMadeShot = null;

        private readonly EcsFilter<WrapperUnityObjectComponent<Text>, OwnerPlayerComponent, IsGunIndicatorComponent>
            _indicators = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _gunsMadeShot)
            {
                ref var gun = ref _gunsMadeShot.GetEntity(i);
                SetAmmoState(gun);
            }

            foreach (var i in _gunsStartReload)
            {
                ref var gun = ref _gunsStartReload.GetEntity(i);
                SetReloadState(gun);

                ref var playerOwner = ref gun.Get<OwnerPlayerComponent>();
                var numberPlayer = playerOwner.PlayerEntity.Get<PlayerComponent>().Number;
                _audioService.StartPlayReloadPlayer(numberPlayer);
            }

            foreach (var i in _gunsEndReload)
            {
                ref var gun = ref _gunsEndReload.GetEntity(i);
                SetAmmoState(gun);
                
                ref var playerOwner = ref gun.Get<OwnerPlayerComponent>();
                var numberPlayer = playerOwner.PlayerEntity.Get<PlayerComponent>().Number;
                
                _audioService.StopPlayReload(numberPlayer);
            }
        }


        private void SetReloadState(in EcsEntity gun)
        {
            var indicator = GetIndicator(gun);
            if (indicator == null) return;
            indicator.text = "RELOAD";
        }

        private void SetAmmoState(in EcsEntity gun)
        {
            var indicator = GetIndicator(gun);
            if (indicator == null) return;

            var ammo = 0;
            if (gun.Has<AmmoComponent>())
            {
                ammo = gun.Get<AmmoComponent>().Value;
            }
            
            ref var ammoCapacity = ref gun.Get<AmmoCapacityComponent>();
            SetAmmoState(indicator, ammo, ammoCapacity.Value);
        }

        private void SetAmmoState(Text indicator, int ammo, int ammoCapacity)
        {
            indicator.text = $"{ammo} / {ammoCapacity}";
        }
       
        private Text GetIndicator(in EcsEntity gun)
        {
            ref var ownerComponent = ref gun.Get<OwnerPlayerComponent>();
            var indicator = GetIndicator(ownerComponent);
            return indicator;
        }

        private Text GetIndicator(in OwnerPlayerComponent ownerPlayerComponent) =>
            _indicators.GetIndicator(ownerPlayerComponent.PlayerEntity.Get<PlayerComponent>().Number);
    }
}