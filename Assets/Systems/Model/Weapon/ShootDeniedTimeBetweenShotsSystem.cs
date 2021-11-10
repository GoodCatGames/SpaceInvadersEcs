using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.Timers;
using SpaceInvadersLeoEcs.Extensions.Systems.Timers;

namespace SpaceInvadersLeoEcs.Systems.Model.Weapon
{
    internal sealed class ShootDeniedTimeBetweenShotsSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<ShootingComponent, Timer<IsTimerBetweenShots>> _filter = null; 
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                _filter.GetEntity(i).Del<ShootingComponent>();
            }
        }
    }
}