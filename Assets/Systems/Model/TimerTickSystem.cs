using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Timers;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class TimerTickSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<TimeRBetweenShotsComponent> _filter = null;
        private readonly EcsFilter<TimeRGunReloadComponent> _filterGunReload = null;

        void IEcsRunSystem.Run()
        {
            MadeTick<EcsFilter<TimeRBetweenShotsComponent>, TimeRBetweenShotsComponent>(_filter);
            MadeTick<EcsFilter<TimeRGunReloadComponent>, TimeRGunReloadComponent>(_filterGunReload);
        }

        private void MadeTick<TFilter, TComponent>(TFilter filter)
            where TFilter : EcsFilter<TComponent>
            where TComponent : struct, ITimer
        {
            foreach (var i in filter)
            {
                ref var timer = ref filter.Get1(i);
                timer.TimeLostSec -= Time.deltaTime;

                if (timer.TimeLostSec <= 0)
                {
                    var entity = filter.GetEntity(i);
                    entity.Del<TComponent>();
                }
            }
        }
    }
}