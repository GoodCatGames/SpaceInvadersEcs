using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Body.Gun;
using Model.Components.Body.Timers;
using Model.Components.Events;
using Model.Components.Events.InputEvents;
using Model.Extensions.Timers;

namespace Model.Systems.Weapon
{
    public sealed class GunReloadStartSystem : IEcsRunSystem
    {
        // auto-injected fields.
        
        // Auto
        private readonly EcsFilter<GunReload, AmmoCapacity>.Exclude<Timer<TimerGunReload>, Ammo>
            _filterGunsNoAmmo = null;

        // Manual
        private readonly EcsFilter<PlayerOwner, GunReload, AmmoCapacity, Ammo>.Exclude<Timer<TimerGunReload>>
            _filterGunsWithAmmo = null;

        private readonly EcsFilter<InputGunReloadEvent> _filterInputReloadGunEvent = null;
        
        void IEcsRunSystem.Run()
        {
            // Manual Reload - Input
            ManualReload(_filterInputReloadGunEvent, _filterGunsWithAmmo);

            // Auto Reload - No Ammo
            AutoReload(_filterGunsNoAmmo);
        }

        private void AutoReload(EcsFilter<GunReload, AmmoCapacity>.Exclude<Timer<TimerGunReload>, Ammo> filterGunsNoAmmo)
        {
            foreach (var i in filterGunsNoAmmo)
            {
                ref var setupComponent = ref filterGunsNoAmmo.Get1(i);
                ref var entity = ref filterGunsNoAmmo.GetEntity(i);
                ReloadStart(entity, setupComponent.TimeReloadSec);
            }
        }

        private void ManualReload(EcsFilter<InputGunReloadEvent> filterInputReloadGunEvent,
            EcsFilter<PlayerOwner, GunReload, AmmoCapacity, Ammo>.Exclude<Timer<TimerGunReload>>
                filterGunsWithAmmo)
        {
            foreach (var i in filterInputReloadGunEvent)
            {
                var playerNumber = filterInputReloadGunEvent.Get1(i).PlayerNumber;

                foreach (var j in filterGunsWithAmmo)
                {
                    ref var owner = ref filterGunsWithAmmo.Get1(j).PlayerEntity;
                    if (owner.Has<Player>()
                        && owner.Get<Player>().Number == playerNumber)
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
            gun.Get<GunReloadStartEvent>();
            gun.Get<GunReloadInProcess>();
            gun.Get<Timer<TimerGunReload>>().TimeLeftSec = timeSec;
        }
    }
    
    
}