namespace SpaceInvadersLeoEcs.Components.Body.Timers
{
    internal struct TimeRBetweenShotsComponent : ITimer
    {
        public float TimeLostSec { get; set; }
    }
}