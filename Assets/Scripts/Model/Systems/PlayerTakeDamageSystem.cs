using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Body.Mob;
using Model.Components.Events.UnityEvents;
using Model.Components.Requests;
using Model.Extensions.Components;

namespace Model.Systems
{
    public sealed class PlayerTakeDamageSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<Mob, ContainerComponents<OnBecameInvisibleEvent>> _filterMobsAbroad = null;
        private readonly EcsFilter<Player> _filterPlayers = null;
        void IEcsRunSystem.Run()
        {
            var countMobs = _filterMobsAbroad.GetEntitiesCount();
            foreach (var i in _filterPlayers)
            {
                ref var entity = ref _filterPlayers.GetEntity(i);
                entity.Get<DamageRequest>().Damage = countMobs;
            }
        }
    }
}