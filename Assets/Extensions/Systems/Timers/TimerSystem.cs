using Leopotam.Ecs;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Extensions.Systems.Timers
{
    public sealed class TimerSystem<TTimerFlag> : IEcsRunSystem
        where TTimerFlag : struct
    {
        // auto-injected fields.
        private readonly EcsFilter<Timer<TTimerFlag>> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var timer = ref _filter.Get1(i);
                timer.TimeLeftSec -= Time.deltaTime;

                if (timer.TimeLeftSec <= 0)
                {
                    _filter.GetEntity(i).Del<Timer<TTimerFlag>>();
                }
            }
        }
    }
}