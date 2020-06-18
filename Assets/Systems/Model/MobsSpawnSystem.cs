using System;
using System.Linq;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Blueprints;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class MobsSpawnSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        private readonly GameContext _gameContext = null;
        
        private readonly EcsFilter<CreateMobsRequest> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                var powerMobs = _filter.Get1(i).PowerMobs;
                CreateMobsSumPower(powerMobs);
            }
        }

        private void CreateMobsSumPower(float powerMobs)
        {
            var lostPower = powerMobs;
            while (TryGetRandomMob(out var mobBlueprint, lostPower))
            {
                var randomXPosition = Random.Range(_gameContext.MinBorderGameField.x, _gameContext.MaxBorderGameField.x);
                CreateMob(mobBlueprint, new Vector2(randomXPosition, _gameContext.MaxBorderGameField.y));
                var powerMob = _gameContext.MobBlueprintPowers[mobBlueprint];
                if (powerMob < 0.1f) throw new Exception("powerMob so weak!"); 
                lostPower -= powerMob;
            }
        }

        private bool TryGetRandomMob(out MobBlueprint mobBlueprint, float maxPower)
        {
            mobBlueprint = null;
            
            var mobBlueprints = _gameContext.MobBlueprintPowers.Where(pair => pair.Value <= maxPower)
                .Select(pair => pair.Key);
            mobBlueprint = mobBlueprints.Random();
            return mobBlueprint != default;
        }
        
        private void CreateMob(MobBlueprint mobBlueprint, Vector2 position)
        {
            var entity = mobBlueprint.CreateEntity(_world);
            entity.Replace(new CreateViewRequest() {StartPosition = position});
        }
    }
}