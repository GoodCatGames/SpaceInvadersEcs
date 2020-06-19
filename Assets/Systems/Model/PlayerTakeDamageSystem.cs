using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Components.Body.Player;
using SpaceInvadersLeoEcs.Components.Events.UnityEvents;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Components;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class PlayerTakeDamageSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<IsMobComponent, ContainerComponents<OnBecameInvisibleEvent>> _filterMobsAbroad = null;
        private readonly EcsFilter<PlayerComponent> _filterPlayers = null;
        void IEcsRunSystem.Run()
        {
            var countMobs = _filterMobsAbroad.GetEntitiesCount();
            foreach (var i in _filterPlayers)
            {
                ref var entity = ref _filterPlayers.GetEntity(i);
                entity.Get<MakeDamageRequest>().Damage = countMobs;
            }
        }
    }
}