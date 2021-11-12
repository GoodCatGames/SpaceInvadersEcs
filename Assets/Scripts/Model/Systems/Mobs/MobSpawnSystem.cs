using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using Model.AppData;
using Model.Components.Body.Mob;
using Model.Components.Requests;
using Model.Extensions;
using Model.Extensions.EntityFactories;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Model.Systems.Mobs
{
    public sealed class MobSpawnSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        private readonly GameContext _gameContext = null;

        private readonly EcsFilter<MobsCreateRequest> _filter = null;

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

                var powerMob = _gameContext.MobFactoryToPowers[mobBlueprint];
                if (powerMob < 0.1f) throw new Exception("powerMob so weak!");
                lostPower -= powerMob;
            }
        }

        private bool TryGetRandomMob(out IEntityFactory mobBlueprint, in float maxPower)
        {
            mobBlueprint = null;
            var mobBlueprints = GetMobBlueprints(maxPower);
            mobBlueprint = mobBlueprints.Random();
            return mobBlueprint != default;
        }

        private List<IEntityFactory> GetMobBlueprints(in float maxPower)
        {
            var result = new List<IEntityFactory>();
            foreach (var pair in _gameContext.MobFactoryToPowers)
            {
                if (pair.Value <= maxPower) result.Add(pair.Key);
            }

            return result;
        }

        private void CreateMob(IEntityFactory mobBlueprint, in Vector2 position)
        {
            var entity = mobBlueprint.CreateEntity(_world);
            var power = _gameContext.MobFactoryToPowers[mobBlueprint];
            entity.Get<PowerGameDesign>() = new PowerGameDesign { Initial = power, Current = power };
            entity.Get<ViewCreateRequest>().StartPosition = position;
        }
    }
}