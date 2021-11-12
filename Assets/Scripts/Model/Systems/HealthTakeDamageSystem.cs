using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Events;
using Model.Components.Requests;
using UnityEngine;

namespace Model.Systems
{
    public sealed class HealthTakeDamageSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<DamageRequest, Health> _filter = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var makeDamageRequest = ref _filter.Get1(i);
                ref var healthCurrent = ref _filter.Get2(i).Current;
                var healthCurrentValue = healthCurrent - makeDamageRequest.Damage;
                healthCurrent = Mathf.Clamp(healthCurrentValue, 0, int.MaxValue);

                ref var entity = ref _filter.GetEntity(i);
                if (healthCurrent != 0)
                {
                    entity.Get<HealthChangeEvent>();
                    entity.Get<ViewUpdateRequest>();
                }
            }
        }
    }
}