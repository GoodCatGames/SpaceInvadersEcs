using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Body.Mob;
using Model.Components.Events;
using SpaceInvadersLeoEcs.Extensions.Components;
using UnityEngine;

namespace SpaceInvadersLeoEcs.View.Systems.Update
{
    internal sealed class MobViewUpdateSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly
            EcsFilter<UnityComponent<SpriteRenderer>, Health, ViewUpdateRequest,
                Mob> _filter = null;
        
        private readonly Color _lowHealthColor = Color.green;
        private readonly Color _middleHealthColor = Color.yellow;
        private readonly Color _highHealthColor = Color.red;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var wrapperUnityObject = ref _filter.Get1(i);
                ref var healthCurrent = ref _filter.Get2(i);
                UpdateView(healthCurrent, wrapperUnityObject);
            }
        }

        private void UpdateView(in Health healthCurrentComponent, in UnityComponent<SpriteRenderer> unityComponent)
        {
            if (healthCurrentComponent.Current == 1) unityComponent.Value.color = _lowHealthColor;
            if (healthCurrentComponent.Current == 2) unityComponent.Value.color = _middleHealthColor;
            if (healthCurrentComponent.Current >= 3) unityComponent.Value.color = _highHealthColor;
        }
    }
}