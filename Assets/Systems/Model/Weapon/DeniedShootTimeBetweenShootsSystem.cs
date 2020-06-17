using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.Timers;

namespace SpaceInvadersLeoEcs.Systems.Model.Weapon
{
    internal sealed class DeniedShootTimeBetweenShootsSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<Shooting, TimeRBetweenShotsComponent> _filter = null; 
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                entity.Del<Shooting>();
            }
        }
    }
}