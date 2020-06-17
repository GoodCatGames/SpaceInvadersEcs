using System.Linq;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class DestroyChildrenDestroyedOwner : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<DestroyEntityRequest> _filterDestroy = null;
        private readonly EcsFilter<OwnerComponent> _filterWithOwners = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterWithOwners)
            {
                var ownerComponent = _filterWithOwners.Get1(i);
                if (_filterDestroy.GetEntitiesToArray().Any(entity => entity == ownerComponent.Entity))
                {
                    var entity = _filterWithOwners.GetEntity(i);
                    entity.Replace(new DestroyEntityRequest());
                }
            }
        }
    }
}