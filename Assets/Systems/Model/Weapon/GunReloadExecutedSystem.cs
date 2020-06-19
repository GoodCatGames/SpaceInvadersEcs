using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.Timers;
using SpaceInvadersLeoEcs.Components.Events;

namespace SpaceInvadersLeoEcs.Systems.Model.Weapon
{
    internal sealed class GunReloadExecutedSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<AmmoCapacityComponent, IsReloadGunInProcessComponent>.Exclude<TimeRGunReloadComponent> _filter = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var ammoCapacity = ref _filter.Get1(i);
                ref var gun = ref _filter.GetEntity(i);
                gun.Get<AmmoComponent>().Value = ammoCapacity.Value;
                gun.Get<IsReloadEndEvent>();
                gun.Del<IsReloadGunInProcessComponent>();
            }
        }
    }
}