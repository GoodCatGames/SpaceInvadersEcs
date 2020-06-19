using Leopotam.Ecs;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Body.GameManager;
using SpaceInvadersLeoEcs.Components.Body.Mob;
using SpaceInvadersLeoEcs.Components.Requests;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class ScenaristSystem : IEcsRunSystem
    {
        private const float TimeUpdateSec = 5f;
        
        // auto-injected fields.
        private readonly EcsWorld _world = null;

        private readonly EcsFilter<PowerGameDesignCurrentComponent, IsMobComponent> _filterMobsInGame = null;
        private readonly EcsFilter<ScoreComponent> _filterScore = null;

        private float _timer;
        
        void IEcsRunSystem.Run()
        {
            // Timer
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
            var powerMobSum = GetPowerMobsInGame(); 
            var powerAdd = powerNeed - powerMobSum;
            if (powerAdd > 0)
            {
                var entity = _world.NewEntity();
                entity.Get<CreateMobsRequest>().PowerMobs = powerAdd;
            }
        }

        private float GetPowerMobsInGame()
        {
            float sum = 0;
            foreach (var i in _filterMobsInGame)
            {
                sum += _filterMobsInGame.Get1(i).Power;
            }

            return sum;
        }
    }
}