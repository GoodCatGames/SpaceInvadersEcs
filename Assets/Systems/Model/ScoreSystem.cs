using System.Linq;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.GameManager;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions.Components;
using SpaceInvadersLeoEcs.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class ScoreSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<PowerGameDesignBase, DestroyEntityRequest, IsMob> _filterDeathMobs = null;
        private readonly EcsFilter<Score, WrapperUnityObject<Text>> _filterScore = null;
        
        void IEcsRunSystem.Run()
        {
            if (!_filterDeathMobs.IsEmpty())
            {
                var sumPower = _filterDeathMobs.Get1ToArray().Sum(power => power.Power);
                ref var score = ref _filterScore.Get1(0);
                ref var wrapper = ref _filterScore.Get2(0);
                score.Value += Mathf.RoundToInt(sumPower);
                wrapper.Value.text = score.Value.ToString();
            }
        }
    }
}