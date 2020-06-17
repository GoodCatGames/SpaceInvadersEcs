using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.Timers;
using SpaceInvadersLeoEcs.Components.Events;

namespace SpaceInvadersLeoEcs.Systems.Model.Weapon
{
    internal sealed class ReloadGunExecutedSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<AmmoCapacity, IsReloadGunInProcess>.Exclude<TimeRGunReloadComponent> _filter = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                var ammoCapacity = _filter.Get1(i);
                var gun = _filter.GetEntity(i);
                gun.Replace(new Ammo() {Value = ammoCapacity.Value});
                gun.Del<IsReloadGunInProcess>();
                gun.Get<IsReloadEndEvent>();
            }
        }
    }
}