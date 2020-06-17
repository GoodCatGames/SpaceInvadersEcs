using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Components.Requests;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Extensions.Systems.CreateView
{
    internal abstract class CreateViewSystem<TComponentFlag> : IEcsRunSystem
        where TComponentFlag : struct
    {
        protected readonly EcsWorld _world = null;
        protected EcsFilter<CreateViewRequest, TComponentFlag>.Exclude<ViewObjectComponent> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                var createViewRequest = _filter.Get1(i);
                var startPosition = createViewRequest.StartPosition;
                ref var entity = ref _filter.GetEntity(i);
                CreateView(ref entity, startPosition);
                entity.Get<IsViewCreatedEvent>();
            }
        }
        
        protected abstract void CreateView(ref EcsEntity entity, Vector3 startPosition);
    }
}