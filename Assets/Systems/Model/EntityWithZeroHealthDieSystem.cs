using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Requests;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class EntityWithZeroHealthDieSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<HealthBaseComponent>.Exclude<HealthCurrentComponent> _filter = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                _filter.GetEntity(i).Get<IsDestroyEntityRequest>();
            }
        }
    }
}