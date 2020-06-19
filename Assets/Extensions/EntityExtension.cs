using System;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Extensions.Components;

namespace SpaceInvadersLeoEcs.Extensions
{
    public static class EntityExtension
    {
        public static void AddEventToStack<T>(in this EcsEntity entity)
            where T : struct
        {
            var eventComponent = new T();
            AddEventToStack(entity, eventComponent);
        }

        public static void AddEventToStack<T>(in this EcsEntity entity, in T eventComponent)
            where T : struct
        {
            if(!entity.IsAlive()) throw new Exception();
            ref var containerComponents = ref entity.Get<ContainerComponents<T>>();
            containerComponents.List.Add(eventComponent);
        }
    }
}