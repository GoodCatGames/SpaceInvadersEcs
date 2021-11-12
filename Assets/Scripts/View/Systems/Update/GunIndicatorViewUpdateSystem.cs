using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Body.Gun;
using Model.Components.Body.UI;
using Model.Components.Events;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.View.Helpers;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.View.Systems.Update
{
    internal sealed class GunIndicatorViewUpdateSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<ShootIsPossible, PlayerOwner, GunReloadStartEvent> _gunsStartReload = null;
        private readonly EcsFilter<ShootIsPossible, PlayerOwner, GunReloadEndEvent> _gunsEndReload = null;
        private readonly EcsFilter<ShootIsPossible, PlayerOwner, ShotMadeEvent> _gunsMadeShot = null;

        private readonly EcsFilter<UnityComponent<Text>, PlayerOwner, GunIndicator>
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
            }

            foreach (var i in _gunsEndReload)
            {
                ref var gun = ref _gunsEndReload.GetEntity(i);
                SetAmmoState(gun);
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
            var ammo = gun.Has<Ammo>() ? gun.Get<Ammo>().Value : 0;
            ref var ammoCapacity = ref gun.Get<AmmoCapacity>();
            SetAmmoState(indicator, ammo, ammoCapacity.Value);
        }

        private void SetAmmoState(Text indicator, int ammo, int ammoCapacity)
        {
            indicator.text = $"{ammo} / {ammoCapacity}";
        }
       
        private Text GetIndicator(in EcsEntity gun)
        {
            ref var ownerComponent = ref gun.Get<PlayerOwner>();
            var indicator = GetIndicator(ownerComponent);
            return indicator;
        }

        private Text GetIndicator(in PlayerOwner playerOwner) =>
            _indicators.GetIndicator(playerOwner.PlayerEntity.Get<Player>().Number);
    }
}