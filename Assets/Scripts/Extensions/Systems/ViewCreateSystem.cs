using Leopotam.Ecs;
using Model.Components.Events;
using SpaceInvadersLeoEcs.Extensions.EntityToGameObject;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Extensions.Systems
{
    public abstract class ViewCreateSystem<TCreateData, TFlag> : IEcsRunSystem
        where TCreateData : struct
        where TFlag : struct
    {
        protected readonly EcsWorld _world = null;
        protected EcsFilter<TCreateData, TFlag> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var createRequest = ref _filter.Get1(i);
                
                // Create or get transform
                var transform = GetTransform(entity, createRequest);
                
                // Bind
                var provider = transform.GetProvider();
                provider.SetEntity(entity);
                
                // Del request
                entity.Del<TCreateData>();

                // You must add .OneFrame<IsUpdateViewRequest>() after all  ViewCreateSystems!
                entity.Get<ViewUpdateRequest>();
            }
        }

        protected abstract Transform GetTransform(in EcsEntity entity, in TCreateData data);
    }
}