using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.WrappersMonoBehaviour;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.UnityComponent;
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

                entity.Get<IsViewCreatedEvent>();
                
                var transform = CreateView(entity, startPosition);
                var provider = transform.GetProvider();
                provider.SetEntity(_world, entity);
            }
        }

        protected abstract UnityEngine.Transform CreateView(in EcsEntity entity, Vector3 startPosition);

        
    }
}