using Leopotam.Ecs;
using Model.Components.Body.Gun;
using Model.Components.Body.Timers;
using Model.Components.Events;
using Model.Extensions.Timers;

namespace Model.Systems.Weapon
{
    public sealed class GunReloadExecutedSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<AmmoCapacity, GunReloadInProcess>.Exclude<Timer<TimerGunReload>> _filter = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var ammoCapacity = ref _filter.Get1(i);
                ref var gun = ref _filter.GetEntity(i);
                gun.Get<Ammo>().Value = ammoCapacity.Value;
                gun.Get<GunReloadEndEvent>();
                gun.Del<GunReloadInProcess>();
            }
        }
    }
}