using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.Timers;

namespace SpaceInvadersLeoEcs.Systems.Model.Weapon
{
    internal sealed class ShootDeniedTimeBetweenShotsSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<ShootingComponent, TimeRBetweenShotsComponent> _filter = null; 
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                _filter.GetEntity(i).Del<ShootingComponent>();
            }
        }
    }
}