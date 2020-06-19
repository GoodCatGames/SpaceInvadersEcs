using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Timers;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class TimerTickSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<TimeRBetweenShotsComponent> _filterTimerBetweenShots = null;
        private readonly EcsFilter<TimeRGunReloadComponent> _filterTimerGunReload = null;

        void IEcsRunSystem.Run()
        {
            MadeTickTimerBetweenShotsComponent();
            MadeTickTimerGunReload();
        }

        private void MadeTickTimerBetweenShotsComponent()
        {
            foreach (var i in _filterTimerBetweenShots)
            {
                ref var timer = ref _filterTimerBetweenShots.Get1(i);
                timer.TimeLostSec -= Time.deltaTime;

                if (timer.TimeLostSec <= 0)
                {
                    _filterTimerBetweenShots.GetEntity(i).Del<TimeRBetweenShotsComponent>();
                }
            }
        }
        
        private void MadeTickTimerGunReload()
        {
            foreach (var i in _filterTimerGunReload)
            {
                ref var timer = ref _filterTimerGunReload.Get1(i);
                timer.TimeLostSec -= Time.deltaTime;

                if (timer.TimeLostSec <= 0)
                {
                    _filterTimerGunReload.GetEntity(i).Del<TimeRGunReloadComponent>();
                }
            }
        }
    }
}