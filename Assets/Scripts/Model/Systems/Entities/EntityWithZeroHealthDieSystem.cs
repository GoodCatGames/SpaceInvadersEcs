using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Requests;

namespace Model.Systems.Entities
{
    public sealed class EntityWithZeroHealthDieSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<Health> _filter = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var health = ref _filter.Get1(i);
                if (health.Current == 0)
                {
                    _filter.GetEntity(i).Get<EntityDestroyRequest>();
                }
            }
        }
    }
}