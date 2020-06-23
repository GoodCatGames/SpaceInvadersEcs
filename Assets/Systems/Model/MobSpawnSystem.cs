using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Blueprints;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Components.Requests;
using SpaceInvadersLeoEcs.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceInvadersLeoEcs.Systems.Model
{
    internal sealed class MobSpawnSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        private readonly GameContext _gameContext = null;

        private readonly EcsFilter<CreateMobsRequest> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                var powerMobs = _filter.Get1(i).PowerMobs;
                CreateMobsSumPower(powerMobs);
            }
        }

        private void CreateMobsSumPower(in float powerMobs)
        {
            var lostPower = powerMobs;
            while (TryGetRandomMob(out var mobBlueprint, lostPower))
            {
                var randomXPosition =
                    Random.Range(_gameContext.MinBorderGameField.x, _gameContext.MaxBorderGameField.x);

                CreateMob(mobBlueprint, new Vector2(randomXPosition, _gameContext.MaxBorderGameField.y));

                var powerMob = _gameContext.MobBlueprintPowers[mobBlueprint];
                if (powerMob < 0.1f) throw new Exception("powerMob so weak!");
                lostPower -= powerMob;
            }
        }

        private bool TryGetRandomMob(out MobBlueprint mobBlueprint, in float maxPower)
        {
            mobBlueprint = null;
            var mobBlueprints = GetMobBlueprints(maxPower);
            mobBlueprint = mobBlueprints.Random();
            return mobBlueprint != default;
        }

        private List<MobBlueprint> GetMobBlueprints(in float maxPower)
        {
            var result = new List<MobBlueprint>();
            foreach (var pair in _gameContext.MobBlueprintPowers)
            {
                if (pair.Value <= maxPower) result.Add(pair.Key);
            }

            return result;
        }

        private void CreateMob(MobBlueprint mobBlueprint, in Vector2 position)
        {
            var entity = mobBlueprint.CreateEntity(_world);
            var power = _gameContext.MobBlueprintPowers[mobBlueprint];
            entity.Get<PowerGameDesignBaseComponent>().Power = power;
            entity.Get<PowerGameDesignCurrentComponent>().Power = power;
            entity.Get<CreateViewRequest>().StartPosition = position;
        }
    }
}