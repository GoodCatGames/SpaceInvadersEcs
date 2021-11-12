namespace Model.Extensions.Timers
{
    public struct Timer<TTimerFlag>
        where TTimerFlag : struct
    {
        public float TimeLeftSec;
    }
}