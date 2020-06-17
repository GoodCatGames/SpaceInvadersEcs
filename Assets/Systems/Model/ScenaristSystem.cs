using System.Linq;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.GameManager;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class ScenaristSystem : IEcsRunSystem
    {
        private const float TimeUpdateSec = 5f;
        
        // auto-injected fields.
        private readonly EcsWorld _world = null;

        private readonly EcsFilter<PowerGameDesignCurrent, IsMob> _filter = null;
        private readonly EcsFilter<Score> _filterScore = null;

        private float _timer;
        
        void IEcsRunSystem.Run()
        {
            // timer
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                return;
            }
            else
            {
                _timer = TimeUpdateSec;
            }
            
            // CreateMobsRequest
            var powerNeed = _filterScore.Get1(0).Value * 0.1f + 10f;
            var powerMobSum = _filter.Get1ToArray().Sum(power => power.Power); 
            var powerAdd = powerNeed - powerMobSum;
            if (powerAdd > 0)
            {
                var entity = _world.NewEntity();
                entity.Replace(new CreateMobsRequest() {PowerMobs = powerAdd});
            }
        }
    }
}