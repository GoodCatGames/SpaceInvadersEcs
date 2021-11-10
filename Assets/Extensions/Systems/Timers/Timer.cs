using Leopotam.Ecs;

namespace SpaceInvadersLeoEcs.Extensions.Systems.Timers
{
    public struct Timer<TTimerFlag>
        where TTimerFlag : struct
    {
        public float TimeLeftSec;
    }
}