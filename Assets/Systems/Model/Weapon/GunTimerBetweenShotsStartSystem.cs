﻿using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Gun;
using SpaceInvadersLeoEcs.Components.Body.Timers;
using SpaceInvadersLeoEcs.Components.Events;

namespace SpaceInvadersLeoEcs.Systems.Model.Weapon
{
    internal sealed class GunTimerBetweenShotsStartSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<TimeBetweenShotsSetupComponent, IsShotMadeEvent> _filter = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var timeBetweenShotsComponent = ref _filter.Get1(i);
                ref var gun = ref _filter.GetEntity(i);
                gun.Get<TimeRBetweenShotsComponent>().TimeLostSec = timeBetweenShotsComponent.TimeSec;
            }
        }
    }
}