using System;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Events;
using SpaceInvadersLeoEcs.Components.Requests;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class DamageHealthSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<MakeDamageRequest, HealthCurrent> _filter = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                var makeDamageRequest = _filter.Get1(i);
                ref var healthCurrent = ref _filter.Get2(i);
                var healthCurrentValue = healthCurrent.Value - makeDamageRequest.Damage;
                healthCurrent.Value = Mathf.Clamp(healthCurrentValue, 0, Int32.MaxValue);

                var entity = _filter.GetEntity(i);
                if (healthCurrent.Value == 0)
                {
                    entity.Del<HealthCurrent>();
                }
                else
                {
                    entity.Replace(new HealthChangeEvent());
                }
            }
        }
    }
}