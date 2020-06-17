using Leopotam.Ecs;
using SpaceInvadersLeoEcs.AppData;
using SpaceInvadersLeoEcs.Blueprints;
using SpaceInvadersLeoEcs.Components.Body;
using SpaceInvadersLeoEcs.Extensions.Blueprints;

namespace SpaceInvadersLeoEcs.Systems.Blueprints
{
    internal sealed class SaveMobPowersSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly GameContext _gameContext = null;
        
        private readonly EcsFilter<BlueprintRefComponent<MobBlueprint>, PowerGameDesignCurrent> _filter = null;
        
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                var blueprintRefComponent = _filter.Get1(i);
                var powerGameDesign = _filter.Get2(i);

                _gameContext.MobBlueprintPowers[blueprintRefComponent.Value] =
                    powerGameDesign.Power;
            }
        }
    }
}