using Leopotam.Ecs;
using Model.Components.Body.Gun;
using Model.Components.Body.Timers;
using Model.Extensions.Timers;

namespace Model.Systems.Weapon
{
    public sealed class ShootDeniedTimeBetweenShotsSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<Shooting, Timer<TimerBetweenShots>> _filter = null; 
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                _filter.GetEntity(i).Del<Shooting>();
            }
        }
    }
}