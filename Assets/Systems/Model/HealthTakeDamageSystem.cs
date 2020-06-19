using System;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Events;
using SpaceInvadersLeoEcs.Components.Requests;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class HealthTakeDamageSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<MakeDamageRequest, HealthCurrentComponent> _filter = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var makeDamageRequest = ref _filter.Get1(i);
                ref var healthCurrent = ref _filter.Get2(i);
                var healthCurrentValue = healthCurrent.Value - makeDamageRequest.Damage;
                healthCurrent.Value = Mathf.Clamp(healthCurrentValue, 0, int.MaxValue);

                var entity = _filter.GetEntity(i);
                if (healthCurrent.Value == 0)
                {
                    entity.Del<HealthCurrentComponent>();
                }
                else
                {
                    entity.Get<IsHealthChangeEvent>();
                }
            }
        }
    }
}