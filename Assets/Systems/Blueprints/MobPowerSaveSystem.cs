using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Blueprints;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Extensions.Blueprints;

namespace SpaceInvadersLeoEcs.Systems.Blueprints
{
    internal sealed class MobPowerSaveSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly GameContext _gameContext = null;
        private readonly EcsFilter<BlueprintRefComponent<MobBlueprint>, PowerGameDesignCurrentComponent> _filter = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var blueprintRefComponent = ref _filter.Get1(i);
                ref var powerGameDesign = ref _filter.Get2(i);

                _gameContext.MobBlueprintPowers[blueprintRefComponent.Value] = powerGameDesign.Power;
            }
        }
    }
}