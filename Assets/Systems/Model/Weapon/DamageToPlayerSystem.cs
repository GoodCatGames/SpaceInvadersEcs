using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Components.Body.Player;
using SpaceInvadersLeoEcs.Components.Events.UnityEvents;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Components;

namespace SpaceInvadersLeoEcs.Systems.Model.Weapon
{
    internal sealed class DamageToPlayerSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<IsMob, ContainerComponents<OnBecameInvisibleEvent>> _filterMobsAbroad = null;
        private readonly EcsFilter<PlayerComponent> _filterPlayers = null;
        void IEcsRunSystem.Run()
        {
            var countMobs = _filterMobsAbroad.GetEntitiesCount();
            foreach (var i in _filterPlayers)
            {
                var entity = _filterPlayers.GetEntity(i);
                entity.Replace(new MakeDamageRequest() {Damage = countMobs});
            }
        }
    }
}