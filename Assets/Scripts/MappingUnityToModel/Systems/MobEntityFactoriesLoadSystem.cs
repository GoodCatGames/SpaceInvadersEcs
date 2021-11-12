using System.Linq;
using Leopotam.Ecs;
using Model.AppData;
using Model.Extensions.EntityFactories;
using SpaceInvadersLeoEcs.MappingUnityToModel.EntityFactoriesFromSo;
using UnityEngine;

namespace SpaceInvadersLeoEcs.MappingUnityToModel.Systems
{
    internal sealed class MobEntityFactoriesLoadSystem : IEcsInitSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        
        private readonly AppConfiguration _appConfiguration = null;
        private readonly GameContext _gameContext = null;
        
        void IEcsInitSystem.Init()
        {
            var mobEntityFactories = Resources.LoadAll<MobEntityFactoryFromSo>(_appConfiguration.mobBlueprintsPath);
            _gameContext.MobFactoryToPowers = mobEntityFactories.ToDictionary
                (factoryFromSo => (IEntityFactory)factoryFromSo, factoryFromSo => 0f);
            
            // Add Entities
            foreach (var key in _gameContext.MobFactoryToPowers.Keys)
            {
                var entity = key.CreateEntity(_world);
                entity.Get<EntityFactoryRef<MobEntityFactoryFromSo>>().Value = (MobEntityFactoryFromSo)key;
            }
        }
    }
}