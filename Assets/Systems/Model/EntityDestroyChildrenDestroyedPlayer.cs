using System.Collections.Generic;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Player;
using SpaceInvadersLeoEcs.Components.Requests;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class EntityDestroyChildrenDestroyedPlayer : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<PlayerComponent, IsDestroyEntityRequest> _filterDestroyPlayers = null;
        private readonly EcsFilter<OwnerPlayerComponent> _filterWithOwners = null;
        void IEcsRunSystem.Run()
        {
            var destroyedPlayers = GetDestroyedPlayers();
            foreach (var i in _filterWithOwners)
            {
                ref var ownerComponent = ref _filterWithOwners.Get1(i);
                if (destroyedPlayers.Contains(ownerComponent.PlayerEntity))
                {
                    ref var entity = ref _filterWithOwners.GetEntity(i);
                    entity.Get<IsDestroyEntityRequest>(); 
                }
            }
        }

        private List<EcsEntity> GetDestroyedPlayers()
        {
            var result = new List<EcsEntity>();
            foreach (var i in _filterDestroyPlayers)
            {
                ref var player = ref _filterDestroyPlayers.GetEntity(i);
                result.Add(player);
            }

            return result;
        }
    }
}