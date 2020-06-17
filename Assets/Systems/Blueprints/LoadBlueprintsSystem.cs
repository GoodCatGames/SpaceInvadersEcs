using System.Linq;
using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Blueprints;
using SpaceInvadersLeoEcs.Extensions.Blueprints;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Systems.Blueprints
{
    internal sealed class LoadBlueprintsSystem : IEcsInitSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        private readonly GameContext _gameContext = null;
        public void Init()
        {
            var mobBlueprints = Resources.LoadAll<MobBlueprint>("Blueprints/Mobs/");
            _gameContext.MobBlueprintPowers = mobBlueprints.ToDictionary
                (blueprint => blueprint, blueprint => 0f);
            
            // Add Entities
            foreach (var key in _gameContext.MobBlueprintPowers.Keys)
            {
                var entity = key.CreateEntity(_world);
                entity.Replace(new BlueprintRefComponent<MobBlueprint>() {Value = key});
            }
        }
    }
}