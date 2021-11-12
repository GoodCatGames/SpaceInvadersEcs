using System.Collections.Generic;
using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Requests;

namespace Model.Systems.Entities
{
    public sealed class EntityDestroyChildrenDestroyedPlayer : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<Player, EntityDestroyRequest> _filterDestroyPlayers = null;
        private readonly EcsFilter<PlayerOwner> _filterWithOwners = null;
        void IEcsRunSystem.Run()
        {
            var destroyedPlayers = GetDestroyedPlayers();
            foreach (var i in _filterWithOwners)
            {
                ref var ownerComponent = ref _filterWithOwners.Get1(i);
                if (destroyedPlayers.Contains(ownerComponent.PlayerEntity))
                {
                    ref var entity = ref _filterWithOwners.GetEntity(i);
                    entity.Get<EntityDestroyRequest>(); 
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