using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Components.Events;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Components;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.View
{
    internal sealed class ChangeMobsViewSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<WrapperUnityObject<SpriteRenderer>, HealthCurrent, HealthChangeEvent, IsMob> _filterChangeHealthMobs = null;
        private readonly EcsFilter<WrapperUnityObject<SpriteRenderer>, HealthCurrent, CreateViewRequest, IsMob> _filterCreateMobs = null;
        
        private readonly Color _lowHealthColor = Color.green;
        private readonly Color _middleHealthColor = Color.yellow;
        private readonly Color _highHealthColor = Color.red;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterChangeHealthMobs)
            {
                var wrapperUnityObject = _filterChangeHealthMobs.Get1(i);
                var healthCurrent = _filterChangeHealthMobs.Get2(i);
                UpdateView(healthCurrent, wrapperUnityObject);
            }
            
            foreach (var i in _filterCreateMobs)
            {
                var wrapperUnityObject = _filterCreateMobs.Get1(i);
                var healthCurrent = _filterCreateMobs.Get2(i);
                UpdateView(healthCurrent, wrapperUnityObject);
            }
        }

        private void UpdateView(HealthCurrent healthCurrent, WrapperUnityObject<SpriteRenderer> wrapperUnityObject)
        {
            if (healthCurrent.Value == 1) wrapperUnityObject.Value.color = _lowHealthColor;
            if (healthCurrent.Value == 2) wrapperUnityObject.Value.color = _middleHealthColor;
            if (healthCurrent.Value >= 3) wrapperUnityObject.Value.color = _highHealthColor;
        }
    }
}