using Leopotam.Ecs;
using Model.AppData;
using Model.Components.Body.Mob;
using Model.Extensions.EntityFactories;
using SpaceInvadersLeoEcs.MappingUnityToModel.EntityFactoriesFromSo;

namespace SpaceInvadersLeoEcs.MappingUnityToModel.Systems
{
    internal sealed class MobPowerSaveSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly GameContext _gameContext = null;
        private readonly EcsFilter<EntityFactoryRef<MobEntityFactoryFromSo>, PowerGameDesign> _filter = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var entityFactoryRef = ref _filter.Get1(i);
                ref var powerGameDesign = ref _filter.Get2(i);

                _gameContext.MobFactoryToPowers[entityFactoryRef.Value] = powerGameDesign.Initial;
            }
        }
    }
}