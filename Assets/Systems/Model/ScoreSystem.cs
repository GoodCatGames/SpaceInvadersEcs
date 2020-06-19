using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.GameManager;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Components;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class ScoreSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<PowerGameDesignBaseComponent, IsDestroyEntityRequest, IsMobComponent> _filterDeathMobs = null;
        private readonly EcsFilter<ScoreComponent, WrapperUnityObjectComponent<Text>> _filterScore = null;
        
        void IEcsRunSystem.Run()
        {
            if (!_filterDeathMobs.IsEmpty())
            {
                var sumPower = GetPowerDiedMobs();
                ref var score = ref _filterScore.Get1(0);
                ref var wrapper = ref _filterScore.Get2(0);
                score.Value += Mathf.RoundToInt(sumPower);
                wrapper.Value.text = score.Value.ToString();
            }
        }
        
        private float GetPowerDiedMobs()
        {
            float sum = 0;
            foreach (var i in _filterDeathMobs)
            {
                sum += _filterDeathMobs.Get1(i).Power;
            }

            return sum;
        }
    }
}