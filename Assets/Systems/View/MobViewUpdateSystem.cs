using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Components.Events;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Components;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.View
{
    internal sealed class MobViewUpdateSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly
            EcsFilter<WrapperUnityObjectComponent<SpriteRenderer>, HealthCurrentComponent, IsHealthChangeEvent,
                IsMobComponent> _filterChangeHealthMobs = null;
        private readonly
            EcsFilter<WrapperUnityObjectComponent<SpriteRenderer>, HealthCurrentComponent, CreateViewRequest,
                IsMobComponent> _filterCreateMobs = null;
        
        private readonly Color _lowHealthColor = Color.green;
        private readonly Color _middleHealthColor = Color.yellow;
        private readonly Color _highHealthColor = Color.red;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterChangeHealthMobs)
            {
                ref var wrapperUnityObject = ref _filterChangeHealthMobs.Get1(i);
                ref var healthCurrent = ref _filterChangeHealthMobs.Get2(i);
                UpdateView(healthCurrent, wrapperUnityObject);
            }
            
            foreach (var i in _filterCreateMobs)
            {
                ref var wrapperUnityObject = ref _filterCreateMobs.Get1(i);
                ref var healthCurrent = ref _filterCreateMobs.Get2(i);
                UpdateView(healthCurrent, wrapperUnityObject);
            }
        }

        private void UpdateView(in HealthCurrentComponent healthCurrentComponent, in WrapperUnityObjectComponent<SpriteRenderer> wrapperUnityObjectComponent)
        {
            if (healthCurrentComponent.Value == 1) wrapperUnityObjectComponent.Value.color = _lowHealthColor;
            if (healthCurrentComponent.Value == 2) wrapperUnityObjectComponent.Value.color = _middleHealthColor;
            if (healthCurrentComponent.Value >= 3) wrapperUnityObjectComponent.Value.color = _highHealthColor;
        }
    }
}