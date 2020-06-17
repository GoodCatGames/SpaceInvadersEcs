using System;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Systems.Model.Data;

namespace SpaceInvadersLeoEcs.Extensions.Systems.Transform
{
    public class Emitter : IEcsSystem
    {
        private readonly EcsFilter<ViewObjectComponent> _filter = null;

        public void AddEvent<T>(UnityEngine.Transform transform, T eventComponent)
            where T : struct
        {
            var entityExist = TryGetEntity(transform, out var entity);
            if (!entityExist) return;
            ref var containerComponents = ref entity.Get<ContainerComponents<T>>();
            containerComponents.List.Add(eventComponent);
        }

        public EcsEntity GetEntity(UnityEngine.Transform transform)
        {
            var entityExist = TryGetEntity(transform, out var entity);
            if (!entityExist) throw new Exception();
            return entity;
        }

        public bool TryGetEntity(UnityEngine.Transform transform, out EcsEntity entity)
        {
            entity = EcsEntity.Null;
            foreach (var i in _filter)
            {
                var viewObjectComponent = _filter.Get1(i);
                var viewObjectUnity = (ViewObjectUnity) viewObjectComponent.ViewObject;
                if (viewObjectUnity.Transform != transform) continue;
                ref var entityRef = ref _filter.GetEntity(i);
                entity = entityRef;
                return true;
            }

            return false;
        }
    }
}