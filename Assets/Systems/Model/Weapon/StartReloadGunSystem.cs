using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.Player;
using SpaceInvadersLeoEcs.Components.Body.Timers;
using SpaceInvadersLeoEcs.Components.Events;
using SpaceInvadersLeoEcs.Components.Events.InputEvents;

namespace SpaceInvadersLeoEcs.Systems.Model.Weapon
{
    internal sealed class StartReloadGunSystem : IEcsRunSystem
    {
        // auto-injected fields.
        
        // Auto
        private readonly EcsFilter<ReloadGunComponent, AmmoCapacity>.Exclude<TimeRGunReloadComponent, Ammo>
            _filterGunsNoAmmo = null;

        // Manual
        private readonly EcsFilter<OwnerComponent, ReloadGunComponent, AmmoCapacity, Ammo>.Exclude<TimeRGunReloadComponent>
            _filterGunsWithAmmo = null;

        private readonly EcsFilter<InputReloadGunEvent> _filterInputReloadGunEvent = null;
        
        void IEcsRunSystem.Run()
        {
            // Manual Reload - Input
            ManualReload(_filterInputReloadGunEvent, _filterGunsWithAmmo);

            // Auto Reload - No Ammo
            AutoReload(_filterGunsNoAmmo);
        }

        private void AutoReload(EcsFilter<ReloadGunComponent, AmmoCapacity>.Exclude<TimeRGunReloadComponent, Ammo> filterGunsNoAmmo)
        {
            foreach (var i in filterGunsNoAmmo)
            {
                var setupComponent = filterGunsNoAmmo.Get1(i);
                var entity = filterGunsNoAmmo.GetEntity(i);
                ReloadStart(entity, setupComponent.TimeReloadSec);
            }
        }

        private void ManualReload(EcsFilter<InputReloadGunEvent> filterInputReloadGunEvent,
            EcsFilter<OwnerComponent, ReloadGunComponent, AmmoCapacity, Ammo>.Exclude<TimeRGunReloadComponent>
                filterGunsWithAmmo)
        {
            foreach (var i in filterInputReloadGunEvent)
            {
                var reloadGunEvent = filterInputReloadGunEvent.Get1(i);
                var playerNumber = reloadGunEvent.PlayerNumber;

                foreach (var j in filterGunsWithAmmo)
                {
                    var owner = filterGunsWithAmmo.Get1(j).Entity;
                    if (owner.Has<PlayerComponent>()
                        && owner.Get<PlayerComponent>().Number == playerNumber)
                    {
                        var setupComponent = filterGunsWithAmmo.Get2(j);
                        var gun = filterGunsWithAmmo.GetEntity(j);
                        ReloadStart(gun, setupComponent.TimeReloadSec);
                    }
                }
            }
        }

        private void ReloadStart(EcsEntity gun, float timeSec)
        {
            gun.Get<IsReloadStartEvent>();
            gun.Get<IsReloadGunInProcess>();
            gun.Replace(new TimeRGunReloadComponent() {TimeLostSec = timeSec});
        }
    }
    
    
}