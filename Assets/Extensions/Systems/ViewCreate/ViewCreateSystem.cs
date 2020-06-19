using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Components.Requests;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Extensions.Systems.ViewCreate
{
    internal abstract class ViewCreateSystem<TComponentFlag> : IEcsRunSystem
        where TComponentFlag : struct
    {
        protected readonly EcsWorld _world = null;
        protected EcsFilter<CreateViewRequest, TComponentFlag>.Exclude<ViewObjectComponent> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                var startPosition = _filter.Get1(i).StartPosition;
                CreateView(entity, startPosition);
                entity.Get<IsViewCreatedEvent>();
            }
        }
        
        protected abstract void CreateView(in EcsEntity entity, Vector3 startPosition);
    }
}