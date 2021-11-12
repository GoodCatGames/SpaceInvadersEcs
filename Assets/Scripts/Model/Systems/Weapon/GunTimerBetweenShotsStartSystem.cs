using Leopotam.Ecs;
using Model.Components.Body.Gun;
using Model.Components.Body.Timers;
using Model.Components.Events;
using Model.Extensions.Timers;

namespace Model.Systems.Weapon
{
    public sealed class GunTimerBetweenShotsStartSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<TimeBetweenShotsSetup, ShotMadeEvent> _filter = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var timeBetweenShotsComponent = ref _filter.Get1(i);
                ref var gun = ref _filter.GetEntity(i);
                gun.Get<Timer<TimerBetweenShots>>().TimeLeftSec = timeBetweenShotsComponent.TimeSec;
            }
        }
    }
}