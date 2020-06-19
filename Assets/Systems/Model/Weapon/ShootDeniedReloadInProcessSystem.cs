using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Gun;

namespace SpaceInvadersLeoEcs.Systems.Model.Weapon
{
    internal sealed class ShootDeniedReloadInProcessSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<ShootingComponent, IsReloadGunInProcessComponent> _filter = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                _filter.GetEntity(i).Del<ShootingComponent>();
            }
        }
    }
}