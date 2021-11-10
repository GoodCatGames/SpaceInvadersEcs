using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.Player;
using SpaceInvadersLeoEcs.Components.Body.Timers;
using SpaceInvadersLeoEcs.Components.Events;
using SpaceInvadersLeoEcs.Components.Events.InputEvents;
using SpaceInvadersLeoEcs.Extensions.Systems.Timers;

namespace SpaceInvadersLeoEcs.Systems.Model.Weapon
{
    internal sealed class GunReloadStartSystem : IEcsRunSystem
    {
        // auto-injected fields.
        
        // Auto
        private readonly EcsFilter<ReloadGunComponent, AmmoCapacityComponent>.Exclude<Timer<IsTimerGunReload>, AmmoComponent>
            _filterGunsNoAmmo = null;

        // Manual
        private readonly EcsFilter<OwnerPlayerComponent, ReloadGunComponent, AmmoCapacityComponent, AmmoComponent>.Exclude<Timer<IsTimerGunReload>>
            _filterGunsWithAmmo = null;

        private readonly EcsFilter<InputReloadGunEvent> _filterInputReloadGunEvent = null;
        
        void IEcsRunSystem.Run()
        {
            // Manual Reload - Input
            ManualReload(_filterInputReloadGunEvent, _filterGunsWithAmmo);

            // Auto Reload - No Ammo
            AutoReload(_filterGunsNoAmmo);
        }

        private void AutoReload(EcsFilter<ReloadGunComponent, AmmoCapacityComponent>.Exclude<Timer<IsTimerGunReload>, AmmoComponent> filterGunsNoAmmo)
        {
            foreach (var i in filterGunsNoAmmo)
            {
                ref var setupComponent = ref filterGunsNoAmmo.Get1(i);
                ref var entity = ref filterGunsNoAmmo.GetEntity(i);
                ReloadStart(entity, setupComponent.TimeReloadSec);
            }
        }

        private void ManualReload(EcsFilter<InputReloadGunEvent> filterInputReloadGunEvent,
            EcsFilter<OwnerPlayerComponent, ReloadGunComponent, AmmoCapacityComponent, AmmoComponent>.Exclude<Timer<IsTimerGunReload>>
                filterGunsWithAmmo)
        {
            foreach (var i in filterInputReloadGunEvent)
            {
                var playerNumber = filterInputReloadGunEvent.Get1(i).PlayerNumber;

                foreach (var j in filterGunsWithAmmo)
                {
                    ref var owner = ref filterGunsWithAmmo.Get1(j).PlayerEntity;
                    if (owner.Has<PlayerComponent>()
                        && owner.Get<PlayerComponent>().Number == playerNumber)
                    {
                        ref var setupComponent = ref filterGunsWithAmmo.Get2(j);
                        ref var gun = ref filterGunsWithAmmo.GetEntity(j);
                        ReloadStart(gun, setupComponent.TimeReloadSec);
                    }
                }
            }
        }

        private void ReloadStart(in EcsEntity gun, in float timeSec)
        {
            gun.Get<IsReloadStartEvent>();
            gun.Get<IsReloadGunInProcessComponent>();
            gun.Get<Timer<IsTimerGunReload>>().TimeLeftSec = timeSec;
        }
    }
    
    
}