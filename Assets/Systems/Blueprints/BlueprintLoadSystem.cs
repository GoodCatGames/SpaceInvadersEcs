using System.Linq;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Blueprints;
using SpaceInvadersLeoEcs.Extensions.Blueprints;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Blueprints
{
    internal sealed class BlueprintLoadSystem : IEcsInitSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        private readonly GameContext _gameContext = null;
        private readonly GameConfiguration _gameConfiguration = null;
        void IEcsInitSystem.Init()
        {
            var mobBlueprints = Resources.LoadAll<MobBlueprint>(_gameConfiguration.mobBlueprintsPath);
            _gameContext.MobBlueprintPowers = mobBlueprints.ToDictionary
                (blueprint => blueprint, blueprint => 0f);
            
            // Add Entities
            foreach (var key in _gameContext.MobBlueprintPowers.Keys)
            {
                var entity = key.CreateEntity(_world);
                entity.Get<BlueprintRefComponent<MobBlueprint>>() = new BlueprintRefComponent<MobBlueprint>() {Value = key};
            }
        }
    }
}