using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Timers;
using SpaceInvadersLeoEcs.Components.Requests;

namespace SpaceInvadersLeoEcs.Systems.Model.Weapon
{
    internal sealed class StartTimersAfterShoot : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<TimeBetweenShotsSetupComponent, IsShotMakeRequest> _filter = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                var timeBetweenShotsComponent = _filter.Get1(i);
                var entity = _filter.GetEntity(i);
                entity.Replace(new TimeRBetweenShotsComponent() {TimeLostSec = timeBetweenShotsComponent.TimeSec});
            }
        }
    }
}