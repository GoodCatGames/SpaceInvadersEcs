using LeopotamGroup.Globals;

namespace SpaceInvadersLeoEcs.Extensions.Systems.Transform
{
    public static class EmitterExtension
    {
        public static void AddEcsEvent<T>(this UnityEngine.Transform transform, T eventComponent)
            where T : struct
        {
            Service<Emitter>.Get().AddEvent(transform, eventComponent);
        }
    }
}